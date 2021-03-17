using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace LandingPage.Controllers
{
    public class AboutController : Controller
    {
        public IActionResult AboutView()
        {
            return View();
        }
    }
}
