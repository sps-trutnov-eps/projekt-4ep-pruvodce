using Microsoft.AspNetCore.Mvc;
using PruvodceProject.Data;
using PruvodceProject.Models;

namespace PruvodceProject.Controllers
{
    public class CrowdSourceController : Controller
    {
        public PruvodceData _databaze;

        public CrowdSourceController(PruvodceData databaze)
        {
            _databaze = databaze;
        }

        public IActionResult Index()
        {
            List<CrowdSourceModel>? crowdSource = _databaze.CrowdSource
                .ToList();

            return View(crowdSource);
        }
    }
}
