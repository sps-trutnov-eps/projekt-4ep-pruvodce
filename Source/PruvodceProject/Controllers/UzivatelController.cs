using Microsoft.AspNetCore.Mvc;
using PruvodceProject.Data;
using PruvodceProject.Interfaces;
using PruvodceProject.Models;

namespace PruvodceProject.Controllers
{
    public class UzivatelController : Controller
    {
        PruvodceData Databaze { get; }

        private readonly IEmailSender _emailSender;

        public UzivatelController(PruvodceData databaze, IEmailSender emailSender)
        {
            Databaze = databaze;
            _emailSender = emailSender;
        }

        [HttpGet]
        public IActionResult Prihlaseni(string chyba = "")
        {
            ViewData["chyba"] = chyba;
            return View();
        }
        
        [HttpPost]
        public IActionResult Prihlaseni(string? mail, string? heslo) 
        {
            if (mail == null || mail.Trim().Length == 0)
                return RedirectToAction("Prihlaseni", new { chyba = "Nebyl zadán e-mail!" });

            if (heslo == null || heslo.Trim().Length == 0)
                return RedirectToAction("Prihlaseni", new { chyba = "Nebylo zadáno heslo!" });

            //if (!mail.Contains("@"))
            //    mail += "@spstrutnov.cz";

            UzivatelModel? hledaneUdaje = Databaze.Uzivatele.FirstOrDefault(n => n.Mail == mail);

            if (hledaneUdaje == null || !BCrypt.Net.BCrypt.Verify(heslo, hledaneUdaje.Heslo))
                return RedirectToAction("Prihlaseni", new { chyba = "Nesprávný e-mail nebo heslo!" });

            HttpContext.Session.SetString("mail", mail);
            HttpContext.Session.SetString("jeAdmin", hledaneUdaje.JeAdmin.ToString());

            string[] strlist = mail.Split("@",StringSplitOptions.RemoveEmptyEntries);

            HttpContext.Session.SetString("jmeno", strlist[0]);
            return RedirectToAction("Index", "Home");
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

            UzivatelModel? hledaneUdaje = Databaze.Uzivatele.FirstOrDefault(n => n.Mail == mail.Trim());

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

            string url = "https://" + HttpContext.Request.Host.Value + "/Uzivatel/Overit?mail=" + mail + "&kod=" + kod;

            string subject = "Ověření e-mailu!";
            string message = "Klikněte na link pro ověření účtu: " + url;

            await _emailSender.SendEmailAsync(mail, subject, message);

            Databaze.OverovaciUdaje.Add(new UzivatelOvereni() { Heslo = heslo, Mail = mail, Trida = trida, KodOvereni = kod });
            await Databaze.SaveChangesAsync();

            return RedirectToAction("Registrace", new { hotovo = "Byl odeslán ověřovací e-mail."});
        }

        [HttpGet]
        public IActionResult ZapomenuteHeslo(string chyba = "", string hotovo = "")
        {
            ViewData["hotovo"] = hotovo;
            ViewData["chyba"] = chyba;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ZapomenuteHeslo(string mail, string heslo, string hesloZnovu)
        {
            UzivatelModel? hledaneUdaje = Databaze.Uzivatele.FirstOrDefault(n => n.Mail == mail.Trim());

            if (hledaneUdaje == null)
            {
                return RedirectToAction("ZapomenuteHeslo", new { chyba = "Uživatel neexistuje." });
            }
            if (heslo != hesloZnovu)
            {
                return RedirectToAction("ZapomenuteHeslo", new { chyba = "Hesla se neshodují." });
            }

            int kod = new Random().Next(1000000, 9999999);

            string url = HttpContext.Request.Host.Value + "/Prihlaseni/OveritZmenaHesla?mail=" + hledaneUdaje.Mail + "&kod=" + kod;

            string subject = "Ověření e-mailu!";
            string message = "Klikněte na link pro ověření účtu: " + url;

            await _emailSender.SendEmailAsync(hledaneUdaje.Mail, subject, message);

            Databaze.OverovaciUdaje.Add(new UzivatelOvereni() { Heslo = heslo, Mail = hledaneUdaje.Mail, Trida = hledaneUdaje.Trida, KodOvereni = kod });
            await Databaze.SaveChangesAsync();

            return RedirectToAction("ZapomenuteHeslo", new { chyba = "Ověřovací email byl odeslán." });
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
            UzivatelModel? uzivatel = Databaze.Uzivatele.FirstOrDefault(n => n.Mail == HttpContext.Session.GetString("mail"));
            ViewData["chyba"] = chyba;
            ViewData["hotovo"] = hotovo;

            if (uzivatel != null)
                return View(uzivatel);

            HttpContext.Session.Clear();
            return RedirectToAction("Prihlaseni");
        }

        [HttpPost]
        public IActionResult Profil(string? zmenitTrida)
        {
            UzivatelModel? uzivatel = Databaze.Uzivatele.FirstOrDefault(n => n.Mail == HttpContext.Session.GetString("mail"));

            if (uzivatel == null)
            {
                HttpContext.Session.Clear();
                return RedirectToAction("Prihlaseni");
            }

            if (zmenitTrida != null)
            {
                //toDo: ověřit přes mail že uživatel je kdo říká a pak změnit heslo.

                uzivatel.Trida = zmenitTrida;

                Databaze.Entry(uzivatel).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                Databaze.SaveChanges();
            }

            return RedirectToAction("Profil", new { hotovo = "Změny byly úspěšně provedeny." });
        }

        [HttpPost]
        public IActionResult ZmenitHeslo(string? zmenitHeslo, string? stareHeslo)
        {
            UzivatelModel? uzivatel = Databaze.Uzivatele.FirstOrDefault(n => n.Mail == HttpContext.Session.GetString("mail"));

            if (uzivatel == null)
                return RedirectToAction("Prihlaseni", new { chyba = "Nejste přihlášeni!" });

            if (zmenitHeslo == null || zmenitHeslo.Trim().Length == 0)
                //toDo: ověřit přes mail že uživatel je kdo říká a pak změnit heslo.
                return RedirectToAction("Profil", new { chyba = "Nové heslo není zadáno!" });

            if (stareHeslo == null || stareHeslo.Trim().Length == 0 || !BCrypt.Net.BCrypt.Verify(stareHeslo, uzivatel.Heslo))
                return RedirectToAction("Profil", new { chyba = "Nesprávné staré heslo!" });

            uzivatel.Heslo = BCrypt.Net.BCrypt.HashPassword(zmenitHeslo);

            Databaze.Entry(uzivatel).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            Databaze.SaveChanges();

            return RedirectToAction("Profil", new { hotovo = "Změny byly úspěšně provedeny." });
        }

        [HttpPost]
        public async Task<IActionResult> SmazatUcet(string stareHeslo)
        {
            UzivatelModel? uzivatel = Databaze.Uzivatele.FirstOrDefault(n => n.Mail == HttpContext.Session.GetString("mail"));

            if (uzivatel == null)
                return RedirectToAction("Prihlaseni", new { chyba = "Nejste přihlášeni!" });

            string heslo = BCrypt.Net.BCrypt.HashPassword(stareHeslo);

            if (!BCrypt.Net.BCrypt.Verify(stareHeslo, uzivatel.Heslo))
            {
                return RedirectToAction("Profil", new { chyba = "Zadali jste špatné heslo!" });
            }

            int kod = new Random().Next(1000000, 9999999);

            string url = "https://" + HttpContext.Request.Host.Value + "/Prihlaseni/OveritSmazani?mail=" + uzivatel.Mail + "&kod=" + kod;

            string subject = "Ověření e-mailu!";
            string message = "Klikněte na link pro ověření účtu: " + url;

            await _emailSender.SendEmailAsync(uzivatel.Mail, subject, message);

            Databaze.OverovaciUdaje.Add(new UzivatelOvereni() { Heslo = heslo, Mail = uzivatel.Mail, Trida = uzivatel.Trida, KodOvereni = kod });
            await Databaze.SaveChangesAsync();

            return RedirectToAction("Profil", new { chyba = "Byl odeslán ověřovací e-mail." });
        }

        [HttpGet]
        public IActionResult Overit(string? mail, int? kod)
        {
            bool jeAdmin = false;
            if (mail == null || kod == null)
                return RedirectToAction("Registrace", new { chyba = "Špatný ověřovací token!" });

            UzivatelOvereni? uzivatel = Databaze.OverovaciUdaje.FirstOrDefault(uziv => uziv.Mail == mail && uziv.KodOvereni == kod && uziv.ExpiraceOvereni > DateTime.Now);

            if (uzivatel == null)
                return RedirectToAction("Registrace", new { chyba = "Špatný ověřovací token!" });

            List<UzivatelModel> vsichniUzivatele = Databaze.Uzivatele.ToList();
            if (vsichniUzivatele.Count == 0) 
                jeAdmin = true;

            Databaze.Uzivatele.Add(new UzivatelModel() { Heslo = uzivatel.Heslo, Mail = uzivatel.Mail, Trida = uzivatel.Trida, JeAdmin = jeAdmin });
            Databaze.OverovaciUdaje.Remove(uzivatel);
            Databaze.SaveChanges();

            return RedirectToAction("Prihlaseni", new { chyba = "Úspěšně ověřeno, účet vytvořen" });
        }

        [HttpGet]
        public IActionResult OveritSmazani(string? mail, int? kod)
        {
            if (mail == null || kod == null)
                return RedirectToAction("Profil", new { chyba = "Špatný ověřovací token!" });

            UzivatelOvereni? uzivatel = Databaze.OverovaciUdaje.FirstOrDefault(n => n.Mail == mail && n.KodOvereni == kod && n.ExpiraceOvereni > DateTime.Now);
            if (uzivatel == null)
                return RedirectToAction("Profil", new { chyba = "Špatný ověřovací token!" });

            UzivatelModel? uzivatelReal = Databaze.Uzivatele.FirstOrDefault(n => n.Mail == mail);
            if (uzivatelReal == null)
                return RedirectToAction("Profil", new { chyba = "Uživatel neexistuje!" });

            Databaze.Uzivatele.Remove(uzivatelReal);
            Databaze.OverovaciUdaje.Remove(uzivatel);
            Databaze.SaveChanges();

            HttpContext.Session.Clear();

            return RedirectToAction("Prihlaseni", new { chyba = "Účet úspěšně smazán!" });
        }

        [HttpGet]
        public IActionResult OveritZmenaHesla(string? mail, int? kod)
        {
            if (mail == null || kod == null)
                return RedirectToAction("Profil", new { chyba = "Špatný ověřovací token!" });

            UzivatelOvereni? uzivatel = Databaze.OverovaciUdaje.FirstOrDefault(n => n.Mail == mail && n.KodOvereni == kod && n.ExpiraceOvereni > DateTime.Now);
            if (uzivatel == null)
                return RedirectToAction("Profil", new { chyba = "Špatný ověřovací token!" });

            UzivatelModel? uzivatelReal = Databaze.Uzivatele.FirstOrDefault(n => n.Mail == mail);
            if (uzivatelReal == null)
                return RedirectToAction("Profil", new { chyba = "Uživatel neexistuje!" });

            uzivatelReal.Heslo = BCrypt.Net.BCrypt.HashPassword(uzivatel.Heslo);

            Databaze.Entry(uzivatelReal).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            Databaze.OverovaciUdaje.Remove(uzivatel);
            Databaze.SaveChanges();

            return RedirectToAction("Prihlaseni", new { chyba = "Heslo úspěšně změněno!" });
        }
    }
}