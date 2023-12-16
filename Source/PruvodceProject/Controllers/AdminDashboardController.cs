using Microsoft.AspNetCore.Mvc;
using PruvodceProject.Data;
using PruvodceProject.Models;
using System.Linq;

namespace PruvodceProject.Controllers
{
    public class AdminDashboardController : Controller
    {
        public PruvodceData _databaze;

        public AdminDashboardController(PruvodceData databaze)
        {
            _databaze = databaze;
        }

        [HttpGet]
        public IActionResult Index()
        {
            if (HttpContext.Session.GetString("jeAdmin") == "True")
                return View();
            else
                return RedirectToAction("Prihlasit", "Prihlaseni", new { chyba = "Nejste přihlášen jako uživatel s administračními právy" });
        }
        [HttpGet]
        public IActionResult PridatClanek()
        {
            if (HttpContext.Session.GetString("jeAdmin") == "True")
                return View();
            else
                return RedirectToAction("Prihlasit", "Prihlaseni", new { chyba = "Nejste přihlášen jako uživatel s administračními právy" });
        }

        [HttpGet]
        public IActionResult SpravovatUzivatele() {
            List<UserModel> uzivatelskeData = _databaze.PrihlasovaciUdaje.ToList();
            List<UserModel> sortedUzivatelskeData = uzivatelskeData.OrderByDescending(o => o.jeAdmin).ToList();

            if (HttpContext.Session.GetString("jeAdmin") == "True")
                return View(sortedUzivatelskeData);
            else
                return RedirectToAction("Prihlasit", "Prihlaseni", new { chyba = "Nejste přihlášen jako uživatel s administračními právy" });

        }
        [HttpGet]
        public IActionResult DeleteUzivatele(Guid ID)
        {
            if (HttpContext.Session.GetString("jeAdmin") != "True")
                return RedirectToAction("Prihlasit", "Prihlaseni", new { chyba = "Nejste přihlášen jako uživatel s administračními právy" });

            UserModel? uzivatel = _databaze.PrihlasovaciUdaje.FirstOrDefault(n => n != null && n.ID == ID);

            if (uzivatel != null && uzivatel.mail != "senkyr@spstrutnov.cz")
            {
                _databaze.PrihlasovaciUdaje.Remove(uzivatel);
                _databaze.SaveChanges();
            }

            return RedirectToAction("SpravovatUzivatele");
        }
        [HttpGet]
        public IActionResult UdelatUzivateleAdminem(Guid ID)
        {
            if (HttpContext.Session.GetString("jeAdmin") != "True")
                return RedirectToAction("Prihlasit", "Prihlaseni", new { chyba = "Nejste přihlášen jako uživatel s administračními právy" });
            UserModel? uzivatel = _databaze.PrihlasovaciUdaje.FirstOrDefault(n => n != null && n.ID == ID);
            
            if (uzivatel != null)
            {
                uzivatel.jeAdmin = true;
                _databaze.Entry(uzivatel).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                _databaze.SaveChanges();
            }


            return RedirectToAction("SpravovatUzivatele");
        }
        [HttpGet]
        public IActionResult OdebratUzivateleAdmina(Guid ID)
        {
            if (HttpContext.Session.GetString("jeAdmin") != "True")
                return RedirectToAction("Prihlasit", "Prihlaseni", new { chyba = "Nejste přihlášen jako uživatel s administračními právy" });

            UserModel? uzivatel = _databaze.PrihlasovaciUdaje.FirstOrDefault(n => n != null && n.ID == ID);
            if (uzivatel != null && uzivatel.mail != "senkyr@spstrutnov.cz")
            {
                uzivatel.jeAdmin = false;

                _databaze.Entry(uzivatel).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                _databaze.SaveChanges();
            }


            return RedirectToAction("SpravovatUzivatele");
        }
        [HttpPost]
        public IActionResult VytvoritUzivatele(string mail, string? trida, string heslo, bool jeAdmin)
        {
            heslo = BCrypt.Net.BCrypt.HashPassword(heslo);
            
            if (trida != null)
                _databaze.PrihlasovaciUdaje.Add(new UserModel() { heslo = heslo, mail = mail, trida = trida, jeAdmin = jeAdmin });
            else
                _databaze.PrihlasovaciUdaje.Add(new UserModel() { heslo = heslo, mail = mail, jeAdmin = jeAdmin });

            
            _databaze.SaveChanges();
            
            return RedirectToAction("SpravovatUzivatele");
        }
        [HttpGet]
        public IActionResult SpravaStiznosti()
        {
            if (HttpContext.Session.GetString("jeAdmin") != "True")
                return RedirectToAction("Prihlasit", "Prihlaseni", new { chyba = "Nejste přihlášen jako uživatel s administračními právy" });


            List<CrowdSourceModel>? crowdSource = _databaze.CrowdSource.ToList();
            List<CrowdSourceModel> crowdSourceSorted = crowdSource.OrderByDescending(o => o.stav).ToList();
            return View(crowdSourceSorted);
        }
        [HttpPost]
        public IActionResult OdpovedetNaStiznost(string ID, string odpoved, string? stav)
        {
            if (HttpContext.Session.GetString("jeAdmin") != "True")
                return RedirectToAction("Prihlasit", "Prihlaseni", new { chyba = "Nejste přihlášen jako uživatel s administračními právy" });
            if (stav == null)
                stav = "čeká na vyřízení";

            CrowdSourceModel? stiznost = _databaze.CrowdSource.FirstOrDefault(n => n.ID.ToString() == ID);

            if (stiznost != null)
            {
                stiznost.odpovedAmina = odpoved;
                stiznost.stav = stav;

                _databaze.Entry(stiznost).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                _databaze.SaveChanges();
            }


            return RedirectToAction("SpravaStiznosti");
        }
        [HttpPost]
        public IActionResult UlozitStravovaciZarizeni(string? nazev, string? adresa, string? odkazNaMenu, string? popis)
        {
            if (nazev == null || adresa == null|| odkazNaMenu == null)
            {
                return RedirectToAction("PridatClanek", new { chyba = "Vyplňte všechny údaje." });
            }

            _databaze.StravovaciZarizeni.Add(new StravovaciZarizeniModel() { nazev = nazev, adresa = adresa, odkazNaMenu = odkazNaMenu, popis = popis });
            _databaze.SaveChanges();
            return RedirectToAction("PridatClanek", new { chyba = "Stravovací zařízení úspěšně vytvořeno." });
            
        }
        [HttpPost]
        public IActionResult PridatAutomat(string budova, string patro, string typ, bool bagety, string budovaID)
        {
            if (budova == null || patro == null || typ == null)
            {
                return RedirectToAction("PridatClanek", new { chyba = "Vyplňte všechny údaje." });
            }

            if (budovaID == null)
            {
                BudovyModel? _budova = _databaze.Budovy.FirstOrDefault(n => n.name == budova);
                if (_budova != null)
                {
                    _databaze.Automaty.Add(new AutomatyModel() { budova = budova, patro = patro, typ = typ, bagety = bagety, budovaID = _budova });
                    _databaze.SaveChanges();
                    return RedirectToAction("PridatClanek", new { chyba = "Budova neexistuje." });
                }
            }
            if (budovaID != null)
            {
                BudovyModel? _budova = _databaze.Budovy.FirstOrDefault(n => n.IdBudovy.ToString() == budovaID);
                if (_budova != null)
                {
                    _databaze.Automaty.Add(new AutomatyModel() { budova = _budova.name, patro = patro, typ = typ, bagety = bagety, budovaID = _budova });
                    _databaze.SaveChanges();
                    return RedirectToAction("PridatClanek", new { chyba = "Budova neexistuje." });
                }
            }

            return RedirectToAction("PridatClanek", new { chyba = "Automat byl úspěšně vytvořen." });
        }
    }
}