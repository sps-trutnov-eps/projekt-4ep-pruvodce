using Microsoft.AspNetCore.Mvc;

namespace PruvodceProject.Controllers
{
    public class TutorialyController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}