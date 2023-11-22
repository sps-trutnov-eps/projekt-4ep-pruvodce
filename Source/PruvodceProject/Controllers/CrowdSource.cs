using Microsoft.AspNetCore.Mvc;
using PruvodceProject.Data;
using PruvodceProject.Models;

namespace PruvodceProject.Controllers
{
    public class CrowdSource : Controller
    {
        public PruvodceData _databaze;

        public CrowdSource(PruvodceData databaze)
        {
            _databaze = databaze;
        }
        [HttpGet]
        public IActionResult NahlasitProblem()
        {
            return View();
        }
    }
}
