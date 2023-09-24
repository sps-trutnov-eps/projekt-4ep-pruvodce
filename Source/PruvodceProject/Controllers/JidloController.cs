using Microsoft.AspNetCore.Mvc;

namespace PruvodceProject.Controllers
{
    public class JidloController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Automaty()
        {
            return View();
        }
    }
}
