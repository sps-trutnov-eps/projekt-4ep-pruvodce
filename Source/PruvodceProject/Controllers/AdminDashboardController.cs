using Microsoft.AspNetCore.Mvc;
using PruvodceProject.Data;
using PruvodceProject.Models;

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
        public IActionResult CrowdSource()
        {
            List<CrowdSourceModel>? crowdSource = _databaze.CrowdSource
                .ToList();

            if (HttpContext.Session.GetString("jeAdmin") == "True")
                return View(crowdSource);
            else
                return RedirectToAction("Prihlasit", new { chyba = "Nejste přihlášen jako admin!" });
        } 
    }
}
