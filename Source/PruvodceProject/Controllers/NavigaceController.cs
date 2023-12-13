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

        public IActionResult UcebnaDetail(Guid id)
        {
            UcebnaModel? ucebna = _databaze.Ucebna
                .FirstOrDefault(u => u.Id == id);

            return View(ucebna);
        }
        
        [HttpGet]
        public UcebnaModel? UcebnaData(string id)
        {
            return _databaze.Ucebna
                .FirstOrDefault(uc => uc.idUcebny == id);
        }
        
        // public IActionResult BudovaDetail(Guid id)
        // {
        //     BudovyModel? budova = _databaze.Budovy
        //         .FirstOrDefault(u => u.IdBudovy == id);
        //
        //     return View(budova);
        // }

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
