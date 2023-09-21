using BCrypt.Net;
using Microsoft.AspNetCore.Mvc;
using PruvodceProject.Data;
using PruvodceProject.Models;

namespace PruvodceProject.Controllers
{
    public class prihlaseniController : Controller
    {
        PruvodceData Databaze { get; }

        public prihlaseniController(PruvodceData databaze)
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
        public IActionResult registrace(string heslo, string heslo_znovu, string mail, string trida)
        {
            if (!mail.Trim().EndsWith("@spstrutnov.cz"))
            {
                HttpContext.Session.SetString("chyba", "E-mail není školní e-mail.");
                return RedirectToAction("registrace");
            }

            UserModel hledane_udaje = Databaze.prihlasovaci_Udaje.Where(n => n.mail == mail.Trim()).FirstOrDefault();

            if (hledane_udaje != null) {
                HttpContext.Session.SetString("chyba", "Uživatel s tímto e-mailem už existuje.");
                return RedirectToAction("registrace");
            }

            // To Do: 
            // udělat list všech tříd a porovnat jestli třída existuje.

            if (trida == null || trida.Trim().Length == 0)
            {
                HttpContext.Session.SetString("chyba", "nebyla zadána třída!");
                return RedirectToAction("registrace");
            }

            if (heslo == null || heslo.Trim().Length == 0)
            {
                HttpContext.Session.SetString("chyba", "nebylo zadáno heslo!");
                return RedirectToAction("registrace");
            }
            if(heslo_znovu == null || heslo_znovu.Trim().Length == 0)
            {
                HttpContext.Session.SetString("chyba", "nebylo zadáno heslo do kolonky znovu heslo!");
                return RedirectToAction("registrace");
            }
            
            if(heslo != heslo_znovu)
            {
                HttpContext.Session.SetString("chyba", "hesla se liší!");
                return RedirectToAction("registrace");
            }

            if(heslo == heslo_znovu)
            {
                heslo = BCrypt.Net.BCrypt.HashPassword(heslo);
            }

            if(mail == null || mail.Trim().Length == 0)
            {
                HttpContext.Session.SetString("chyba", "nebyl zadán e-mail!");
                return RedirectToAction("registrace");
            }

            

            Databaze.prihlasovaci_Udaje.Add(new UserModel() { heslo = heslo,mail = mail, trida = trida});
            Databaze.SaveChanges();

            return RedirectToAction("prihlasit");
        }

        [HttpPost]
        public IActionResult prihlasit(string mail, string heslo) 
        { 
            UserModel hledane_udaje = Databaze.prihlasovaci_Udaje.Where(n => n.mail == mail).FirstOrDefault();

            if (hledane_udaje != null && BCrypt.Net.BCrypt.Verify(heslo, hledane_udaje.heslo))
            {
                HttpContext.Session.SetString("prihlasen", "Ano");
                HttpContext.Session.SetString("mail", mail);
                return RedirectToAction("Index", "Home");
            }
            return RedirectToAction("prihlasit");
        }

        [HttpGet]
        public IActionResult odhlasit()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult profil()
        {
            if (HttpContext.Session.GetString("prihlasen") == "Ano")
            {
                return View();
            }
            return RedirectToAction("prihlasit");
        }
    }
}
