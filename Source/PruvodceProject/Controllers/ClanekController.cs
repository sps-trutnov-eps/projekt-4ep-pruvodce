using Microsoft.AspNetCore.Mvc;
using PruvodceProject.Data;
using PruvodceProject.Models;

namespace PruvodceProject.Controllers
{
    public class ClanekController : Controller
    {
        PruvodceData Databaze { get; }
        public string Mail;
        public Guid Uzivatel;

        public ClanekController(PruvodceData databaze) => Databaze = databaze;
        
        [HttpGet]
        public IActionResult Editor()
        {
            if(HttpContext.Session.GetString("mail") == null || HttpContext.Session.GetString("mail").Length == 0)
            {
                return RedirectToAction("Prihlaseni","Uzivatel", new {chyba = "Musíte se přihlásit!"});
            }
            return View();
        }
        [HttpPost]
        public IActionResult Ulozit(string nadpis, string text)
        {
            Mail = HttpContext.Session.GetString("mail");
            UzivatelModel? hledaneUdaje = Databaze.Uzivatele.FirstOrDefault(n => n != null && n.Mail == Mail);
            Uzivatel = (hledaneUdaje.Id);
            Databaze.Clanek.Add(new ClanekModel() { AutorId = Uzivatel, NadpisClanku = nadpis, ObsahClanku = text, DatumVytvoreni = DateTime.Now});
            Databaze.SaveChanges();
            return RedirectToAction("Prehled");
        }
        [HttpGet]
        public IActionResult Upravit(Guid id)
        {
            return RedirectToAction("Editor");
        }
        [HttpPost]
        public IActionResult Smazat(Guid id)
        {
            ClanekModel? hledanyClanek = Databaze.Clanek.FirstOrDefault(n => n != null && n.Id == id);
            Databaze.Clanek.Remove(hledanyClanek);
            Databaze.SaveChanges();
            return RedirectToAction("Prehled");
        }
        [HttpGet]
        public IActionResult Prehled() 
        { 
            return View(Databaze.Clanek.ToList());
        }
    }
}