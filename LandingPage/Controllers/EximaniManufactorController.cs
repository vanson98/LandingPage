using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LandingPage.Controllers
{
    public class EximaniManufactorController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
