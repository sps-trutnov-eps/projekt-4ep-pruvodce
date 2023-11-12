using Microsoft.AspNetCore.Mvc;

namespace PruvodceProject.Controllers
{
    public class NavigaceController : Controller
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
        
        public IActionResult Zachody()
        {
            return View();
        }
    }
}
