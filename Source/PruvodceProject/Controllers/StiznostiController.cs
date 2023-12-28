using Microsoft.AspNetCore.Mvc;
using PruvodceProject.Data;
using PruvodceProject.Models;

namespace PruvodceProject.Controllers
{
    public class StiznostiController : Controller
    {
        PruvodceData Databaze { get; }
        public StiznostiController(PruvodceData databaze) => Databaze = databaze;

        [HttpGet]
        public IActionResult NahlasitProblem()
        {
            if (HttpContext.Session.GetString("jmeno") != null)
                return View();
            return RedirectToAction("Index", "Home");
        }
        
        [HttpPost]
        public IActionResult NahlasitProblem(string title, string text)
        {
            if (HttpContext.Session.GetString("mail") == null || HttpContext.Session.GetString("mail").Length == 0)
                return RedirectToAction("Prihlaseni", "Uzivatel", new { chyba = "Nejste přihlášeni!" });

            bool uzivatelNezadalNadpis = title == null || title.Trim().Length == 0;
            bool uzivatelNezadalText = text == null || text.Trim().Length == 0;

            if (uzivatelNezadalNadpis || uzivatelNezadalText)
                return RedirectToAction("NahlasitProblem", new { chyba = "Vyplňte všechny pole!" });
            
            StiznostiModel problem = new StiznostiModel()
            {
                UzivatelMail = HttpContext.Session.GetString("mail"),
                Nadpis = title,
                Text = text,
                Stav = "čeká na vyřízení",
                Existujici = ""
            };
            
            Databaze.Stiznosti.Add(problem);
            Databaze.SaveChanges();

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult Stiznosti()
        {
            if (HttpContext.Session.GetString("mail") == null || HttpContext.Session.GetString("mail").Length == 0)
                return RedirectToAction("Prihlaseni", "Uzivatel", new { chyba = "Nejste přihlášeni!" });

            List<StiznostiModel>? crowdSource = Databaze.Stiznosti.ToList();
            var purged = crowdSource.Where(n => n.UzivatelMail == HttpContext.Session.GetString("mail")); // proč tohle nemůže být List<CrowdSourceModel> ????????
            List<StiznostiModel> crowdSourceSorted = purged.OrderByDescending(o => o.Stav).ToList();
            return View(crowdSourceSorted);
        }
    }
}
