using Microsoft.AspNetCore.Mvc;
using PruvodceProject.Data;
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
            return View();
        }
        [HttpPost]
        public IActionResult NahlasitProblem(string title, string text)
        {
            bool uzivatelNezadalNadpis = title == null || title.Trim().Length == 0;
            bool uzivatelNezadalText = text == null || text.Trim().Length == 0;

            if (uzivatelNezadalNadpis || uzivatelNezadalText)
                return RedirectToAction("Pridat");


            string userMail = HttpContext.Session.GetString("mail");

            UserModel? user = _databaze.PrihlasovaciUdaje
                .Where(u => u.mail == userMail)
                .FirstOrDefault();

            Guid userID = user.ID;

            CrowdSourceModel problem = new CrowdSourceModel()
            {
                IDUzivatele = userID,
                nadpis = title,
                Text = text,
                stav = "čeká na vyřízení",
                existujici = ""
            };

            _databaze.Add(problem);
            _databaze.SaveChanges();

            return RedirectToAction("Index", "Home");
        }
    }
}   
