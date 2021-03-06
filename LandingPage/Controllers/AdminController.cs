using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace LandingPage.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult BlogManager()
        {
            return View();
        }
    }
}
