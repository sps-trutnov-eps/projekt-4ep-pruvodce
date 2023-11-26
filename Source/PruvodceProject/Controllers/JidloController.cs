using Microsoft.AspNetCore.Mvc;
using PruvodceProject.Data;
using PruvodceProject.Models;

namespace PruvodceProject.Controllers
{
    public class JidloController : Controller
    {
        PruvodceData Databaze { get; }
        
        public JidloController(PruvodceData databaze)
        {
            Databaze = databaze;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Automaty()
        {
            List<AutomatyModel> automaty = Databaze.Automaty.ToList();
            automaty.Add(new AutomatyModel()
            {
                budova = "Školní 101",
                patro = "1",
                typ = "Automat na jídlo",
                bagety = true

            }); 
            return View(automaty);
        }

        public IActionResult Kafe()
        {
            return View();
        }

        public IActionResult Obchody()
        {
            return View();
        }

        public IActionResult Mikrovlnky()
        {
            return View();
        }

        public IActionResult Jidelny()
        {
            return View(Databaze.StravovaciZarizeni.ToList());
        }
    }
}
