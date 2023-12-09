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
                return RedirectToAction("Prihlasit", new { chyba = "Nejste přihlášen jako admin!" });
        }

        [HttpGet]
        public IActionResult SpravovatUzivatele() {
            List<UserModel> uzivatelskeData = _databaze.PrihlasovaciUdaje.ToList();
            List<UserModel> sortedUzivatelskeData = uzivatelskeData.OrderByDescending(o => o.jeAdmin).ToList();

            if (HttpContext.Session.GetString("jeAdmin") == "True")
                return View(sortedUzivatelskeData);
            else
                return RedirectToAction("Prihlasit", new { chyba = "Nejste přihlášen jako admin!" });

        }
        [HttpGet]
        public IActionResult DeleteUzivatele(Guid ID)
        {
            if (HttpContext.Session.GetString("jeAdmin") != "True")
                return RedirectToAction("Prihlasit", new { chyba = "Nejste přihlášen jako admin!" });

            UserModel? uzivatel = _databaze.PrihlasovaciUdaje.FirstOrDefault(n => n != null && n.ID == ID);

            if (uzivatel != null)
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
                return RedirectToAction("Prihlasit", new { chyba = "Nejste přihlášen jako admin!" });
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
                return RedirectToAction("Prihlasit", new { chyba = "Nejste přihlášen jako admin!" });

            UserModel? uzivatel = _databaze.PrihlasovaciUdaje.FirstOrDefault(n => n != null && n.ID == ID);
            if (uzivatel != null)
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
                return RedirectToAction("Prihlasit", new { chyba = "Nejste přihlášen jako admin!" });


            List<CrowdSourceModel>? crowdSource = _databaze.CrowdSource.ToList();
            List<CrowdSourceModel> crowdSourceSorted = crowdSource.OrderByDescending(o => o.stav).ToList();
            return View(crowdSourceSorted);
        }
        [HttpPost]
        public IActionResult OdpovedetNaStiznost(string ID, string odpoved, string? stav)
        {
            if (HttpContext.Session.GetString("jeAdmin") != "True")
                return RedirectToAction("Prihlasit", new { chyba = "Nejste přihlášen jako admin!" });
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
    }
}
