using System.Text.Encodings.Web;
using System.Web;
using BCrypt.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;
using PruvodceProject.Data;
using PruvodceProject.Interfaces;
using PruvodceProject.Models;


namespace PruvodceProject.Controllers
{
    public class PrihlaseniController : Controller
    {
        PruvodceData Databaze { get; }

        private readonly IEmailSender _emailSender;

        public PrihlaseniController(PruvodceData databaze, IEmailSender emailSender)
        {
            Databaze = databaze;
            this._emailSender = emailSender;
        }

        [HttpGet]
        public IActionResult Prihlasit(string chyba = "")
        {
            ViewData["chyba"] = chyba;
            return View();
        }

        [HttpGet]
        public IActionResult Registrace(string chyba = "", string hotovo = "")
        {
            ViewData["hotovo"] = hotovo;
            ViewData["chyba"] = chyba;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Registrace(string? heslo, string? hesloZnovu, string? mail, string trida = "")
        {
            if (mail == null || mail.Trim().Length == 0)
                return RedirectToAction("Registrace", new { chyba = "Nebyl zadán e-mail!" });

            if (!mail.Trim().EndsWith("@spstrutnov.cz"))
                return RedirectToAction("Registrace", new { chyba = "E-mail není školní e-mail." });

            UserModel? hledaneUdaje = Databaze.PrihlasovaciUdaje.FirstOrDefault(n => n != null && n.mail == mail.Trim());

            if (hledaneUdaje != null)
                return RedirectToAction("Registrace", new { chyba = "Uživatel s tímto e-mailem už existuje." });

            // ToDo: udělat list všech tříd a porovnat jestli třída existuje.

            if (heslo == null || heslo.Trim().Length == 0)
                return RedirectToAction("Registrace", new { chyba = "Nebylo zadáno heslo!" });

            if (hesloZnovu == null || hesloZnovu.Trim().Length == 0)
                return RedirectToAction("Registrace", new { chyba = "Nebylo zadáno heslo do kolonky znovu heslo!" });
            
            if (heslo != hesloZnovu)
                return RedirectToAction("Registrace", new { chyba = "Hesla se liší!" });

            heslo = BCrypt.Net.BCrypt.HashPassword(heslo);

            int kod = new Random().Next(1000000, 9999999);

            string URL = HttpContext.Request.Host.Value + "/Prihlaseni/Overit?mail=" + mail + "&kod=" + kod;

            string subject = "Ověření e-mailu!";
            string message = "Klikněte na link pro ověření účtu: " + URL;

            await _emailSender.SendEmailAsync(mail, subject, message);

            Databaze.OverovaciUdaje.Add(new UserVerify() { heslo = heslo, mail = mail, trida = trida, kod = kod });
            Databaze.SaveChanges();

            return RedirectToAction("Registrace", new { hotovo = "Byl odeslán ověřovací e-mail."});
        }

        [HttpPost]
        public IActionResult Prihlasit(string? mail, string heslo) 
        {
            if (mail == null || mail.Trim().Length == 0)
                return RedirectToAction("Prihlasit", new { chyba = "Nebyl zadán e-mail!" });

            if (heslo == null || heslo.Trim().Length == 0)
                return RedirectToAction("Prihlasit", new { chyba = "Nebylo zadáno heslo!" });

            if (!mail.Contains("@"))
                mail += "@spstrutnov.cz";

            UserModel? hledaneUdaje = Databaze.PrihlasovaciUdaje.FirstOrDefault(n => n != null && n.mail == mail);

            if (hledaneUdaje == null || !BCrypt.Net.BCrypt.Verify(heslo, hledaneUdaje.heslo))
                return RedirectToAction("Prihlasit", new { chyba = "Nesprávný e-mail nebo heslo!" });

            HttpContext.Session.SetString("mail", mail);

            string[] strlist = mail.Split("@",StringSplitOptions.RemoveEmptyEntries);

            HttpContext.Session.SetString("jmeno", strlist[0]);
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult Odhlasit()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult Profil(string chyba = "", string hotovo = "")
        {
            UserModel? uzivatel = Databaze.PrihlasovaciUdaje.FirstOrDefault(n => n != null && n.mail == HttpContext.Session.GetString("mail"));
            ViewData["chyba"] = chyba;
            ViewData["hotovo"] = hotovo;

            if (uzivatel != null)
                return View(uzivatel);
            
            return RedirectToAction("Prihlasit");
        }

        [HttpPost]
        public IActionResult Profil(string? zmenitTrida, string? zmenitHeslo, string? stareHeslo)
        {
            UserModel? uzivatel = Databaze.PrihlasovaciUdaje.FirstOrDefault(n => n != null && n.mail == HttpContext.Session.GetString("mail"));

            if (uzivatel == null)
                return RedirectToAction("Prihlasit");

            if (zmenitHeslo != null && zmenitHeslo.Trim().Length != 0)
            {
                //toDo: ověřit přes mail že uživatel je kdo říká a pak změnit heslo.

                if (stareHeslo == null || stareHeslo.Trim().Length == 0 || !BCrypt.Net.BCrypt.Verify(stareHeslo, uzivatel.heslo))
                    return RedirectToAction("Profil", new { chyba = "Nesprávné staré heslo!" });

                uzivatel.heslo = BCrypt.Net.BCrypt.HashPassword(zmenitHeslo);

                Databaze.Entry(uzivatel).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                Databaze.SaveChanges();
            }

            if (zmenitTrida != null)
            {
                //toDo: ověřit přes mail že uživatel je kdo říká a pak změnit heslo.

                uzivatel.trida = zmenitTrida;

                Databaze.Entry(uzivatel).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                Databaze.SaveChanges();
            }

            return RedirectToAction("Profil", new { hotovo = "Změny byly úspěšně provedeny." });
        }

        [HttpGet]
        public IActionResult Overit(string? mail, int? kod)
        {
            if (mail == null || kod == null)
                return RedirectToAction("Registrace", new { chyba = "Špatný ověřovací token!" });

            UserVerify? uzivatel = Databaze.OverovaciUdaje.FirstOrDefault(n => n != null && n.mail == mail && n.kod == kod && n.expirace > DateTime.Now);

            if (uzivatel == null)
                return RedirectToAction("Registrace", new { chyba = "Špatný ověřovací token!" });

            Databaze.PrihlasovaciUdaje.Add(new UserModel() { heslo = uzivatel.heslo, mail = uzivatel.mail, trida = uzivatel.trida });
            Databaze.OverovaciUdaje.Remove(uzivatel);
            Databaze.SaveChanges();

            return RedirectToAction("Prihlasit", new { chyba = "Úspěšně ověřeno, účet vytvořen" });
        }
    }
}