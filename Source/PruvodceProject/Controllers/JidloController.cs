using Microsoft.AspNetCore.Mvc;
using PruvodceProject.Data;
using PruvodceProject.Models;
using System.Data.SqlClient;
using System.Data;

namespace PruvodceProject.Controllers
{
    public class JidloController : Controller
    {
        PruvodceData Databaze { get; }
        
        public JidloController(PruvodceData databaze)
        {
            Databaze = databaze;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Automaty()
        {
            return View(Databaze.Automaty.ToList());
        }

        public IActionResult Kafe()
        {
            List<KafeModel> kavarny = Databaze.Kafe.ToList();
            return View(kavarny);
        }


        public IActionResult Obchody()
        {
            return View(Databaze.Obchod.ToList());
        }

        public IActionResult Jidelny()
        {
            return View(Databaze.StravovaciZarizeni.ToList());
        }
        
        // public IActionResult JidelnyDetil(Guid id)
        // {
        //     StravovaciZarizeniModel? stravovaciZarizeni = Databaze.StravovaciZarizeni
        //         .FirstOrDefault(u => u.ID == id);
        //
        //     return View(stravovaciZarizeni);
        // }
    }
}
