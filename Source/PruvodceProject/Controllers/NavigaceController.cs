using Microsoft.AspNetCore.Mvc;
using PruvodceProject.Data;
using PruvodceProject.Models;

namespace PruvodceProject.Controllers
{
    public class NavigaceController : Controller
    {
        public PruvodceData _databaze;
        public NavigaceController(PruvodceData databaze)
        {
            _databaze = databaze;
        }
        
        public IActionResult Index()
        {
            return View();
        }
        
        public IActionResult Ucebny()
        {
            return View(_databaze.Ucebna.ToList());
        }

        public IActionResult Budovy()
        {
            return View(_databaze.Budovy.ToList());
        }

        public IActionResult StravovaciZarizeni()
        {
            return View(_databaze.StravovaciZarizeni.ToList());
        }

        public IActionResult Automaty()
        {
            return View(_databaze.Automaty.ToList());
        }

        public IActionResult UcebnaDetail(Guid id)
        {
            UcebnaModel? ucebna = _databaze.Ucebna
                .FirstOrDefault(u => u.Id == id);

            return View(ucebna);
        }
        
        [HttpGet]
        public UcebnaModel? UcebnaData(string id)
        {
            UcebnaModel? ucebna = _databaze.Ucebna
                .FirstOrDefault(u => u.idUcebny == id);
            return ucebna;
        }
        public IActionResult BudovaDetail(Guid id)
        {
            BudovyModel? budova = _databaze.Budovy
                .FirstOrDefault(u => u.IdBudovy == id);

            return View(budova);
        }
        public IActionResult StravovaciZarizeniDetil(Guid id)
        {
            StravovaciZarizeniModel? stravovaciZarizeni = _databaze.StravovaciZarizeni
                .FirstOrDefault(u => u.ID == id);

            return View(stravovaciZarizeni);
        }
        public IActionResult AutomatyDetail(Guid id)
        {
            AutomatyModel? automat = _databaze.Automaty
                .FirstOrDefault(u => u.ID == id);

            return View(automat);
        }
        public IActionResult Telocvicny()
        {
            return View();
        }
        
        public IActionResult Zachody()
        {
            return View();
        }
        
    }

}
