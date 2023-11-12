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
            return View();
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
            List<StravovaciZarizeniModel> stravovaciZarizeni = Databaze.StravovaciZarizeni.ToList();
            stravovaciZarizeni.Add(new StravovaciZarizeniModel()
            {
                adresa = "Školní 101, Trutnov",
                nazev = "SPŠ",
                odkazNaMenu = "www.odkaz.eu",
                popis = "Dobře vaří :)"
            });

            return View(stravovaciZarizeni);
        }
    }
}
