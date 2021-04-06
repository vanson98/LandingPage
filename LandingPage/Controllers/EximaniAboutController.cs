using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace LandingPage.Controllers
{
    public class EximaniAboutController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
