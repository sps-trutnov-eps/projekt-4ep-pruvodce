using Microsoft.AspNetCore.Mvc;

namespace PruvodceProject.Controllers
{
    public class BudovyController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
