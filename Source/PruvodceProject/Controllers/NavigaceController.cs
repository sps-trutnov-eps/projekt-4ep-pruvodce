using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
                .Include(u => u.Budova) 
                .ThenInclude(pB => pB.fotky)
                .FirstOrDefault(u => u.Id == id);

            //ucebna.Budova = _databaze.Budovy.Where(u => u.IdBudovy == ucebna.BudovaID).FirstOrDefault();
            return View(ucebna);
        }
        
        [HttpGet]
        public UcebnaModel? UcebnaData(string id)
        {
            UcebnaModel? ucebna = _databaze.Ucebna
                .FirstOrDefault(u => u.Nazev == id);

            return ucebna;
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
