﻿using Microsoft.AspNetCore.Mvc;

namespace Neatburguer.Controllers
{
    public class HomeController : Controller
    {
        public HomeController() 
        { 
        
        }

        public IActionResult Index()
        {
            return View();
        }

    }
}
