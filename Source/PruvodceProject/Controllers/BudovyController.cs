using Microsoft.AspNetCore.Mvc;

namespace PruvodceProject.Controllers
{
    public class BudovyController : Controller
    {
        public IActionResult Index()
        {
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
