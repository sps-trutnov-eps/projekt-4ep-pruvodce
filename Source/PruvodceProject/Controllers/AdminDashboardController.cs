﻿using Microsoft.AspNetCore.Mvc;
using PruvodceProject.Data;
using PruvodceProject.Models;

namespace PruvodceProject.Controllers
{
    public class AdminDashboardController : Controller
    {
        public PruvodceData _databaze;

        public AdminDashboardController(PruvodceData databaze)
        {
            _databaze = databaze;
        }

        [HttpGet]
        public IActionResult Index()
        {
            if (HttpContext.Session.GetString("jeAdmin") == "True")
                return View();
            else
                return RedirectToAction("Prihlasit", new { chyba = "Nejste přihlášen jako admin!" });
        }
        [HttpGet]
        public IActionResult CrowdSource()
        {
            List<CrowdSourceModel>? crowdSource = _databaze.CrowdSource.ToList();

            if (HttpContext.Session.GetString("jeAdmin") == "True")
                return View(crowdSource);
            else
                return RedirectToAction("Prihlasit", new { chyba = "Nejste přihlášen jako admin!" });
        }

        [HttpGet]
        public IActionResult SpravovatUzivatele() {
            List<UserModel> uzivatelskeData = _databaze.PrihlasovaciUdaje.ToList();

            if (HttpContext.Session.GetString("jeAdmin") == "True")
                return View(uzivatelskeData);
            else
                return RedirectToAction("Prihlasit", new { chyba = "Nejste přihlášen jako admin!" });

        }
        [HttpGet]
        public IActionResult DeleteUzivatele(Guid ID)
        {
            if (HttpContext.Session.GetString("jeAdmin") != "True")
                return RedirectToAction("Prihlasit", new { chyba = "Nejste přihlášen jako admin!" });

            UserModel? uzivatel = _databaze.PrihlasovaciUdaje.FirstOrDefault(n => n != null && n.ID == ID);

            if (uzivatel == null)
            {
                _databaze.PrihlasovaciUdaje.Remove(uzivatel);
                _databaze.SaveChanges();
            }

            return RedirectToAction("SpravovatUzivatele");
        }
        [HttpGet]
        public IActionResult UdelatUzivateleAdminem(Guid ID)
        {
            if (HttpContext.Session.GetString("jeAdmin") != "True")
                return RedirectToAction("Prihlasit", new { chyba = "Nejste přihlášen jako admin!" });
            UserModel? uzivatel = _databaze.PrihlasovaciUdaje.FirstOrDefault(n => n != null && n.ID == ID);
            
            if (uzivatel != null)
            {
                uzivatel.jeAdmin = true;
                _databaze.Entry(uzivatel).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                _databaze.SaveChanges();
            }


            return RedirectToAction("SpravovatUzivatele");
        }
        [HttpGet]
        public IActionResult OdebratUzivateleAdmina(Guid ID)
        {
            if (HttpContext.Session.GetString("jeAdmin") != "True")
                return RedirectToAction("Prihlasit", new { chyba = "Nejste přihlášen jako admin!" });

            UserModel? uzivatel = _databaze.PrihlasovaciUdaje.FirstOrDefault(n => n != null && n.ID == ID);
            if (uzivatel != null)
            {
                uzivatel.jeAdmin = false;

                _databaze.Entry(uzivatel).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                _databaze.SaveChanges();
            }


            return RedirectToAction("SpravovatUzivatele");
        }
    }
}
