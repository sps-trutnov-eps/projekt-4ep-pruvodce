﻿using Microsoft.AspNetCore.Mvc;

namespace PruvodceProject.Controllers
{
    public class UserController : Controller
    {
        public IActionResult AdminDashboard()
        {
            return View();
        }
    }
}
