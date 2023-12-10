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
    }
}
