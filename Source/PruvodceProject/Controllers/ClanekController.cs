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
            if(HttpContext.Session.GetString("mail") == null || HttpContext.Session.GetString("mail").Length == 0)
            {
                return RedirectToAction("Prihlasit","prihlaseni", new {chyba = "Musíte se přihlásit!"});
            }
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
        public IActionResult upravit(Guid Id)
        {
            return RedirectToAction("editor");
        }
        [HttpPost]
        public IActionResult smazat(Guid Id)
        {
            ClanekModel? hledany_clanek = _databaze.Clanek.FirstOrDefault(n => n != null && n.Id == Id);
            _databaze.Clanek.Remove(hledany_clanek);
            _databaze.SaveChanges();
            return RedirectToAction("prehled");
        }
        [HttpGet]
        public IActionResult prehled() 
        { 
            return View(_databaze.Clanek.ToList());
        }
    }
}