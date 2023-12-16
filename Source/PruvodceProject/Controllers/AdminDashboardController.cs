using Microsoft.AspNetCore.Mvc;
using PruvodceProject.Data;
using PruvodceProject.Models;

namespace PruvodceProject.Controllers
{
    public class AdminDashboardController : Controller
    {
        PruvodceData Databaze { get; }
        public AdminDashboardController(PruvodceData databaze) => Databaze = databaze;

        [HttpGet]
        public IActionResult Index()
        {
            if (HttpContext.Session.GetString("jeAdmin") == "True")
                return View();
            else
                return RedirectToAction("Prihlaseni", "Uzivatel", new { chyba = "Nejste přihlášen jako uživatel s administračními právy" });
        }
        [HttpGet]
        public IActionResult PridatClanek()
        {
            if (HttpContext.Session.GetString("jeAdmin") == "True")
                return View();
            else
                return RedirectToAction("Prihlaseni", "Uzivatel", new { chyba = "Nejste přihlášen jako uživatel s administračními právy" });
        }

        [HttpGet]
        public IActionResult SpravovatUzivatele() {
            List<UzivatelModel> uzivatelskeData = Databaze.Uzivatele.ToList();
            List<UzivatelModel> sortedUzivatelskeData = uzivatelskeData.OrderByDescending(o => o.JeAdmin).ToList();

            if (HttpContext.Session.GetString("jeAdmin") == "True")
                return View(sortedUzivatelskeData);
            else
                return RedirectToAction("Prihlaseni", "Uzivatel", new { chyba = "Nejste přihlášen jako uživatel s administračními právy" });

        }
        [HttpGet]
        public IActionResult DeleteUzivatele(Guid id)
        {
            if (HttpContext.Session.GetString("jeAdmin") != "True")
                return RedirectToAction("Prihlaseni", "Uzivatel", new { chyba = "Nejste přihlášen jako uživatel s administračními právy" });

            UzivatelModel? uzivatel = Databaze.Uzivatele.FirstOrDefault(n => n != null && n.Id == id);

            if (uzivatel != null && uzivatel.Mail != "senkyr@spstrutnov.cz")
            {
                Databaze.Uzivatele.Remove(uzivatel);
                Databaze.SaveChanges();
            }

            return RedirectToAction("SpravovatUzivatele");
        }
        [HttpGet]
        public IActionResult UdelatUzivateleAdminem(Guid id)
        {
            if (HttpContext.Session.GetString("jeAdmin") != "True")
                return RedirectToAction("Prihlaseni", "Uzivatel", new { chyba = "Nejste přihlášen jako uživatel s administračními právy" });
            UzivatelModel? uzivatel = Databaze.Uzivatele.FirstOrDefault(n => n != null && n.Id == id);
            
            if (uzivatel != null)
            {
                uzivatel.JeAdmin = true;
                Databaze.Entry(uzivatel).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                Databaze.SaveChanges();
            }


            return RedirectToAction("SpravovatUzivatele");
        }
        [HttpGet]
        public IActionResult OdebratUzivateleAdmina(Guid id)
        {
            if (HttpContext.Session.GetString("jeAdmin") != "True")
                return RedirectToAction("Prihlaseni", "Uzivatel", new { chyba = "Nejste přihlášen jako uživatel s administračními právy" });

            UzivatelModel? uzivatel = Databaze.Uzivatele.FirstOrDefault(n => n != null && n.Id == id);
            if (uzivatel != null && uzivatel.Mail != "senkyr@spstrutnov.cz")
            {
                uzivatel.JeAdmin = false;

                Databaze.Entry(uzivatel).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                Databaze.SaveChanges();
            }


            return RedirectToAction("SpravovatUzivatele");
        }
        [HttpPost]
        public IActionResult VytvoritUzivatele(string mail, string? trida, string heslo, bool jeAdmin)
        {
            heslo = BCrypt.Net.BCrypt.HashPassword(heslo);
            
            if (trida != null)
                Databaze.Uzivatele.Add(new UzivatelModel() { Heslo = heslo, Mail = mail, Trida = trida, JeAdmin = jeAdmin });
            else
                Databaze.Uzivatele.Add(new UzivatelModel() { Heslo = heslo, Mail = mail, JeAdmin = jeAdmin });

            
            Databaze.SaveChanges();
            
            return RedirectToAction("SpravovatUzivatele");
        }
        [HttpGet]
        public IActionResult SpravaStiznosti()
        {
            if (HttpContext.Session.GetString("jeAdmin") != "True")
                return RedirectToAction("Prihlaseni", "Uzivatel", new { chyba = "Nejste přihlášen jako uživatel s administračními právy" });


            List<StiznostiModel>? crowdSource = Databaze.Stiznosti.ToList();
            List<StiznostiModel> crowdSourceSorted = crowdSource.OrderByDescending(o => o.Stav).ToList();
            return View(crowdSourceSorted);
        }
        [HttpPost]
        public IActionResult OdpovedetNaStiznost(string id, string odpoved, string? stav)
        {
            if (HttpContext.Session.GetString("jeAdmin") != "True")
                return RedirectToAction("Prihlaseni", "Uzivatel", new { chyba = "Nejste přihlášen jako uživatel s administračními právy" });
            if (stav == null)
                stav = "čeká na vyřízení";

            StiznostiModel? stiznost = Databaze.Stiznosti.FirstOrDefault(n => n.Id.ToString() == id);

            if (stiznost != null)
            {
                stiznost.AdminOdpoved = odpoved;
                stiznost.Stav = stav;

                Databaze.Entry(stiznost).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                Databaze.SaveChanges();
            }


            return RedirectToAction("SpravaStiznosti");
        }
    }
}
