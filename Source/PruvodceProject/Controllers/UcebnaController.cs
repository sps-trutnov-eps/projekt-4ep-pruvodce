using Microsoft.AspNetCore.Mvc;
using PruvodceProject.Data;
using PruvodceProject.Models;
using System.Diagnostics;

namespace PruvodceProject.Controllers
{
    public class UcebnaController : Controller
    {
        public PruvodceData _pruvodceData;

        public IActionResult Index()
        {
           

            return View();
        }
        public Ucebna UcebnaDetail(Guid id)
        {
            Ucebna? ucebna = _pruvodceData.Ucebna
                .Where(u => u.Id == id)
                .FirstOrDefault();

            Debug.WriteLine(id);
            Debug.WriteLine(ucebna);

            return ucebna;
        }
    }
}
