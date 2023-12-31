using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PruvodceProject.Data;
using PruvodceProject.Models;

namespace PruvodceProject.Controllers
{
    public class NavigaceController : Controller
    {
        PruvodceData Databaze { get; }
        public NavigaceController(PruvodceData databaze) => Databaze = databaze;

        public IActionResult Ucebny()
        {
            List<UcebnaModel>? ucebna = Databaze.Ucebny
                .Include(u => u.Budova)
                .ThenInclude(pB => pB.Fotky)
                .Include(u => u.Fotky)
                .Where(n => n.Druh == "u�ebna")
                .ToList();
            foreach(UcebnaModel ucebnicka in ucebna)
            {
                ucebnicka.Nazev = ucebnicka.Nazev.Replace("U�ebna_", "");
            }
            return View(ucebna);
        }

        [HttpGet]
        public IActionResult UcebnaDetail(Guid id)
        {
            UcebnaModel? ucebna = Databaze.Ucebny
                .Include(u => u.Budova)
                .ThenInclude(pB => pB.Fotky)
                .Include(u => u.Fotky)
                .FirstOrDefault(u => u.Id == id);
            return View(ucebna);
        }
        
        [HttpGet]
        public UcebnaModel? UcebnaData(string id)
        {
            return Databaze.Ucebny.FirstOrDefault(u => u.Nazev == id);;
        }
        
        [HttpGet]
        [Route("Budovy/{budova?}")]
        public IActionResult Budovy(string? budova)
        {
            ViewData["Budova"] = budova ?? "skolni101";
            return View();
        }
        
        // public IActionResult BudovaDetail(Guid id)
        // {
        //     return View(_databaze.Budovy.FirstOrDefault(u => u.IdBudovy == id));
        // }
    }
}
