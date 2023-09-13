using BCrypt.Net;
using Microsoft.AspNetCore.Mvc;
using PruvodceProject.Data;
using PruvodceProject.Models;

namespace PruvodceProject.Controllers
{
    public class prihlaseniController : Controller
    {
        DatovyKontext Databaze { get; }

        public prihlaseniController(DatovyKontext databaze)
        {
            Databaze = databaze;
        }

        [HttpGet]
        public IActionResult prihlasit()
        {
            return View();
        }

        [HttpGet]
        public IActionResult registrace()
        {
            return View();
        }

        [HttpPost]
        public IActionResult registrace(string prezdivka, string heslo, string heslo_znovu, string e_mail)
        {
            if(prezdivka == null || prezdivka.Trim().Length == 0)
            {
                return RedirectToAction("registrace");
            }
            if(heslo == null || heslo.Trim().Length == 0)
            {
                return RedirectToAction("registrace");
            }
            if(heslo_znovu == null || heslo_znovu.Trim().Length == 0)
            {
                return RedirectToAction("registrace");
            }

            if(heslo == heslo_znovu)
            {
                heslo = BCrypt.Net.BCrypt.HashPassword(heslo);
            }

            if(e_mail == null || e_mail.Trim().Length == 0)
            {
                return RedirectToAction("registrace");
            }

            Databaze.prihlasovaci_Udaje.Add(new prihlasovaci_udaje() { prihlas_jmeno = prezdivka, heslo = heslo,mail = e_mail});
            Databaze.SaveChanges();

            return RedirectToAction("prihlasit");
        }

        [HttpPost]
        public IActionResult prihlasit(string prezdivka, string heslo) 
        { 
            prihlasovaci_udaje hledane_udaje = Databaze.prihlasovaci_Udaje.Where(n => n.prihlas_jmeno == prezdivka).FirstOrDefault();

            if (hledane_udaje != null && BCrypt.Net.BCrypt.Verify(heslo, hledane_udaje.heslo))
            {
                HttpContext.Session.SetString("prihlasen", "Ano");
                HttpContext.Session.SetString("uzivatel", prezdivka);
                return RedirectToAction("Index", "Home");
            }
            return RedirectToAction("prihlasit");
        }
    }
}
