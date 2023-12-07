using Microsoft.AspNetCore.Mvc;
using PruvodceProject.Data;
using PruvodceProject.Models;
using System.Diagnostics;

namespace PruvodceProject.Controllers
{
    public class UcebnaController : Controller
    {
        public PruvodceData _databaze;
        public UcebnaController(PruvodceData databaze)
        {
            _databaze = databaze;
        }


        public IActionResult Index()
        {
            return View();
        }
        
        public UcebnaModel UcebnaDetail(Guid id)
        {
            UcebnaModel? ucebna = _databaze.Ucebna
                .Where(u => u.Id == id)
                .FirstOrDefault();

            return ucebna;
        }
        [HttpGet]
        public UcebnaModel NajitUcebnu(string id)
        {
            UcebnaModel? ucebna = _databaze.Ucebna
                .Where(u => u.idUcebny == id)
                .FirstOrDefault();

            return ucebna;
        }

    }
}
