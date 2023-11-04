using Microsoft.AspNetCore.Mvc;

namespace PruvodceProject.Controllers
{
    public class BudovyController : Controller
    {
        [Route("/Budovy/{budova?}")]
        public IActionResult Index(string? budova)
        {
            ViewData["Budova"] = budova ?? "skolni101";
            return View();
        }

        public IActionResult Ucebny()
        {
            return View();
        }

        public IActionResult Telocvicny()
        {
            return View();
        }

        public IActionResult Jidelny()
        {
            return View();
        }
        public IActionResult Zachody()
        {
            return View();
        }
    }
}
