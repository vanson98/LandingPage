using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LandingPage.Controllers
{
    public class AdminBlogController : Controller
    {
        public IActionResult Index()
        {
            return View("~/Views/Admin/Blogs/Index.cshtml");
        }
        public IActionResult CreateItem()
        {
            return View("~/Views/Admin/Blogs/CreateItem.cshtml");
        }
    }
}
