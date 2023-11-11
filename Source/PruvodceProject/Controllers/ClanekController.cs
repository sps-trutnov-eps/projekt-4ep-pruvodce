using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging.Abstractions;
using PruvodceProject.Data;
using PruvodceProject.Models;

namespace PruvodceProject.Controllers
{
    public class ClanekController : Controller
    {
        public PruvodceData _databaze;
        public string mail;
        public Guid uzivatel;

        public ClanekController(PruvodceData databaze)
        {
            _databaze = databaze;
        }
        [HttpGet]
        public IActionResult editor()
        {
            return View();
        }
        [HttpPost]
        public IActionResult ulozit(string _nadpis, string _text)
        {
            mail = HttpContext.Session.GetString("mail");
            UserModel? hledane_udaje = _databaze.PrihlasovaciUdaje.FirstOrDefault(n => n != null && n.mail == mail);
            uzivatel = (hledane_udaje.ID);
            _databaze.Clanek.Add(new ClanekModel() { ID_autora = uzivatel, NadpisClanku = _nadpis, ObsahClanku = _text, DatumVytvoreni = DateTime.Now});
            _databaze.SaveChanges();
            return RedirectToAction("prehled");
        }
        [HttpGet]
        public IActionResult upravit()
        {
            return RedirectToAction("editor");
        }

        public IActionResult smazat()
        {
            return View();
        }
        [HttpGet]
        public IActionResult prehled() 
        { 
            return View(_databaze.Clanek.ToList());
        }
    }
}
