using Microsoft.AspNetCore.Mvc;
using PruvodceProject.Data;
using PruvodceProject.Models;

namespace PruvodceProject.Controllers
{
    public class prihlaseniController : Controller
    {

        [HttpGet]
        public IActionResult prihlasit()
        {
            return View();
        }

        [HttpGet]
        public IActionResult registrace()
        {
            return View();
        }
    }
}
