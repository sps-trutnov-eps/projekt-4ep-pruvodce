using Microsoft.AspNetCore.Mvc;
using PruvodceProject.Data;
using PruvodceProject.Models;

namespace PruvodceProject.Controllers
{
    public class AdminController : Controller
    {
        PruvodceData Databaze { get; }
        public AdminController(PruvodceData databaze) => Databaze = databaze;

        [HttpGet]
        public IActionResult PridatClanek()
        {
            if (HttpContext.Session.GetString("jeAdmin") != "True")
                return RedirectToAction("Index", "Home");
            return View();
        }

        [HttpGet]
        public IActionResult SpravaUzivatelu() {
            if (HttpContext.Session.GetString("jeAdmin") != "True")
                return RedirectToAction("Index", "Home");
            return View(Databaze.Uzivatele
                .OrderByDescending(o => o.JeAdmin).ToList());
        }
        
        [HttpGet]
        public IActionResult OdebratUzivatele(Guid id)
        {
            if (HttpContext.Session.GetString("jeAdmin") != "True")
                return RedirectToAction("Index", "Home");

            UzivatelModel? uzivatel = Databaze.Uzivatele.FirstOrDefault(n => n.Id == id);

            if (uzivatel != null && uzivatel.Mail != "senkyr@spstrutnov.cz")
            {
                Databaze.Uzivatele.Remove(uzivatel);
                Databaze.SaveChanges();
            }

            return RedirectToAction("SpravaUzivatelu");
        }
        
        [HttpGet]
        public IActionResult PovysitUzivatele(Guid id)
        {
            if (HttpContext.Session.GetString("jeAdmin") != "True")
                return RedirectToAction("Index", "Home");
            UzivatelModel? uzivatel = Databaze.Uzivatele.FirstOrDefault(n => n.Id == id);
            
            if (uzivatel != null)
            {
                uzivatel.JeAdmin = true;
                Databaze.Entry(uzivatel).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                Databaze.SaveChanges();
            }

            return RedirectToAction("SpravaUzivatelu");
        }
        
        [HttpGet]
        public IActionResult PonizitUzivatele(Guid id)
        {
            if (HttpContext.Session.GetString("jeAdmin") != "True")
                return RedirectToAction("Index", "Home");

            UzivatelModel? uzivatel = Databaze.Uzivatele.FirstOrDefault(n => n.Id == id);
            if (uzivatel != null && uzivatel.Mail != "senkyr@spstrutnov.cz")
            {
                uzivatel.JeAdmin = false;

                Databaze.Entry(uzivatel).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                Databaze.SaveChanges();
            }

            return RedirectToAction("SpravaUzivatelu");
        }
        
        [HttpPost]
        public IActionResult VytvoritUzivatele(string mail, string? trida, string heslo, bool jeAdmin)
        {
            if (HttpContext.Session.GetString("jeAdmin") != "True")
                return RedirectToAction("Index", "Home");
            
            heslo = BCrypt.Net.BCrypt.HashPassword(heslo);
            Databaze.Uzivatele.Add(new UzivatelModel() { Heslo = heslo, Mail = mail, Trida = trida ?? "", JeAdmin = jeAdmin });
            Databaze.SaveChanges();
            
            return RedirectToAction("SpravaUzivatelu");
        }
        
        [HttpGet]
        public IActionResult SpravaStiznosti()
        {
            if (HttpContext.Session.GetString("jeAdmin") != "True")
                return RedirectToAction("Index", "Home");

            return View(Databaze.Stiznosti.OrderByDescending(o => o.Stav).ToList());
        }
        
        [HttpPost]
        [Route("OdpovedetNaStiznost/{id}")]
        public IActionResult OdpovedetNaStiznost(string id, string odpoved, string? stav)
        {
            if (HttpContext.Session.GetString("jeAdmin") != "True")
                return RedirectToAction("Prihlaseni", "Uzivatel", new { chyba = "Nejste přihlášen jako uživatel s administračními právy" });
            stav ??= "Čeká na vyřízení";

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
        
        [HttpPost]
        public IActionResult PridatStravovaciZarizeni(string? nazev, string? adresa, string? odkazNaMenu, string? popis)
        {
            if (nazev == null || adresa == null|| odkazNaMenu == null)
                return RedirectToAction("PridatClanek", new { chyba = "Vyplňte všechny údaje." });

            Databaze.StravovaciZarizeni.Add(new StravovaciZarizeniModel() { Nazev = nazev, Adresa = adresa, OdkazNaMenu = odkazNaMenu, Popis = popis ?? "" });
            Databaze.SaveChanges();
            
            return RedirectToAction("PridatClanek", new { chyba = "Stravovací zařízení úspěšně vytvořeno." });
        }
        
        [HttpPost]
        public IActionResult PridatAutomat(string? budova, string? patro, string? typ, bool bagety, string? budovaId)
        {
            if (budova == null || patro == null || typ == null)
                return RedirectToAction("PridatClanek", new { chyba = "Vyplňte všechny údaje." });

            if (budovaId == null)
            {
                BudovyModel? budovaDb = Databaze.Budovy.FirstOrDefault(n => n.name == budova);
                if (budovaDb != null)
                {
                    Databaze.Automaty.Add(new AutomatyModel() { Budova = budova, Patro = patro, Typ = typ, Bagety = bagety, BudovaId = budovaDb });
                    Databaze.SaveChanges();
                    return RedirectToAction("PridatClanek", new { chyba = "Budova neexistuje." });
                }
            }

            if (budovaId == null)
                return RedirectToAction("PridatClanek", new { chyba = "Automat byl úspěšně vytvořen." });
            {
                BudovyModel? budovaDb = Databaze.Budovy.FirstOrDefault(n => n.IdBudovy.ToString() == budovaId);
                if (budovaDb != null)
                {
                    Databaze.Automaty.Add(new AutomatyModel() { Budova = budovaDb.name, Patro = patro, Typ = typ, Bagety = bagety, BudovaId = budovaDb });
                    Databaze.SaveChanges();
                    return RedirectToAction("PridatClanek", new { chyba = "Budova neexistuje." });
                }
            }

            return RedirectToAction("PridatClanek", new { chyba = "Automat byl úspěšně vytvořen." });
        }
    }
}