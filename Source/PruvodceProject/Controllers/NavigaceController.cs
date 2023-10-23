using Microsoft.AspNetCore.Mvc;

namespace PruvodceProject.Controllers
{
    public class NavigaceController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
