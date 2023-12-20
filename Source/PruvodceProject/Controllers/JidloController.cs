using Microsoft.AspNetCore.Mvc;
using PruvodceProject.Data;

namespace PruvodceProject.Controllers
{
    public class JidloController : Controller
    {
        PruvodceData Databaze { get; }
        public JidloController(PruvodceData databaze) => Databaze = databaze;

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Automaty()
        {
            return View(Databaze.Automaty.ToList());
        }

        public IActionResult Jidelny()
        {
            return View(Databaze.StravovaciZarizeni.ToList());
        }
    }
}
