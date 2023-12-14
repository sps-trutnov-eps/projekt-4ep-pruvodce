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

        [HttpGet]
        public IActionResult UcebnaDetail(Guid id)
        {
            UcebnaModel ucebna = _databaze.Ucebna
                .Include(u => u.Budova)
                .ThenInclude(pB => pB.fotky)
                .Include(u => u.fotky)
                .FirstOrDefault(u => u.Id == id);
            return View(ucebna);
        }
        
        [HttpGet]
        public UcebnaModel? UcebnaData(string id)
        {
            return _databaze.Ucebna.FirstOrDefault(u => u.Nazev == id);;
        }
        
        // public IActionResult BudovaDetail(Guid id)
        // {
        //     return View(_databaze.Budovy.FirstOrDefault(u => u.IdBudovy == id));
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
