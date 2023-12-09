using Microsoft.AspNetCore.Mvc;
using PruvodceProject.Data;
using PruvodceProject.Migrations;
using PruvodceProject.Models;
using System.Diagnostics;

namespace PruvodceProject.Controllers
{
    public class CrowdSourceController : Controller
    {
        public PruvodceData _databaze;

        public CrowdSourceController(PruvodceData databaze)
        {
            _databaze = databaze;
        }


        [HttpGet]
        public IActionResult NahlasitProblem()
        {
            if (HttpContext.Session.GetString("jmeno") != null)
                return View();
            else
                return RedirectToAction("Index", "Home");
        }
        [HttpPost]
        public IActionResult NahlasitProblem(string title, string text)
        {
            if (HttpContext.Session.GetString("mail") == null || HttpContext.Session.GetString("mail").Length == 0)
                return RedirectToAction("Prihlasit", "Prihlaseni", new { chyba = "Nejste přihlášeni!" });

            bool uzivatelNezadalNadpis = title == null || title.Trim().Length == 0;
            bool uzivatelNezadalText = text == null || text.Trim().Length == 0;

            if (uzivatelNezadalNadpis || uzivatelNezadalText)
                return RedirectToAction("NahlasitProblem", new { chyba = "Vyplňte všechny pole!" });
            
            CrowdSourceModel problem = new CrowdSourceModel()
            {
                mailUzivatele = HttpContext.Session.GetString("mail").ToString(),
                nadpis = title,
                Text = text,
                stav = "čeká na vyřízení",
                existujici = ""
            };
            
            _databaze.CrowdSource.Add(problem);
            _databaze.SaveChanges();

            return RedirectToAction("Index", "Home");
        }

        public IActionResult Index()
        {
            //Membership.GetUser();
            if (HttpContext.Session.GetString("jeAdmin") == "True")
                return View(_databaze.PrihlasovaciUdaje.ToList());

            return RedirectToAction("Prihlasit", "Prihlaseni", new { chyba = "Nedostatečná oprávnění!" });
        }
        [HttpGet]
        public IActionResult Stiznosti()
        {
            if (HttpContext.Session.GetString("mail") == null || HttpContext.Session.GetString("mail").Length == 0)
                return RedirectToAction("Prihlasit", "Prihlaseni", new { chyba = "Nejste přihlášeni!" });


            List<CrowdSourceModel>? crowdSource = _databaze.CrowdSource.ToList();
            var purged = crowdSource.Where(n => n.mailUzivatele == HttpContext.Session.GetString("mail")); // proč tohle nemůže být List<CrowdSourceModel> ????????
            List<CrowdSourceModel> crowdSourceSorted = purged.OrderByDescending(o => o.stav).ToList();
            return View(crowdSourceSorted);
        }
    }
}
