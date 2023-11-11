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
            _databaze.CrowdSource.Add(new CrowdSourceModel() { IDUzivatele = uzivatel, nadpis = _nadpis, Text = _text, stav = "nevyřízeno"});
            return RedirectToAction("editor");
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
    }
}
