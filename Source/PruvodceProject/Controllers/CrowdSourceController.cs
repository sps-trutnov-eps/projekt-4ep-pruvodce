using Microsoft.AspNetCore.Mvc;
using PruvodceProject.Data;
using PruvodceProject.Models;

namespace PruvodceProject.Controllers
{
    public class CrowdSourceController : Controller
    {
        public PruvodceData _databaze;

        public CrowdSourceController(PruvodceData databaze)
        {
            _databaze = databaze;
        }

        public IActionResult Reporty()
        {
            if (HttpContext.Session.GetString("jeAdmin") == "True")
                return View(_databaze.CrowdSource.ToList());
            
            return RedirectToAction("Prihlasit", "Prihlaseni", new { chyba = "Nedostatečná oprávnění!" });

        }
        
        public IActionResult Index()
        {
            //Membership.GetUser();
            if (HttpContext.Session.GetString("jeAdmin") == "True")
                return View(_databaze.PrihlasovaciUdaje.ToList());
            
            return RedirectToAction("Prihlasit", "Prihlaseni", new { chyba = "Nedostatečná oprávnění!" });
        }
    }
}
