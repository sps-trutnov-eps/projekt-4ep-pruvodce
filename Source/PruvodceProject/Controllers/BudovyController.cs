
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
    }
}