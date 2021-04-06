using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LandingPage.Controllers
{
    public class AdminProductCategoryController : Controller
    {
        public IActionResult Index()
        {

            return View();
        }
        public IActionResult CreateOrUpdate()
        {
            return View();
        }
        public IActionResult Delete()
        {
            return View();
        }
    }
}
