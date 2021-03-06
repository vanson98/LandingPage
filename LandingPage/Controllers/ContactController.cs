using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LandingPage.Models;
using Microsoft.AspNetCore.Mvc;

namespace LandingPage.Controllers
{
    public class ContactController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult ContactView(ContactViewModel contact)
        {
          
            return View();
        }

        [HttpPost]
        public IActionResult ReceiveEmail(string email)
        {
            var mail = email;
            var isReceive = false;
            if (mail!=null){
                isReceive = true;
            }
            return Json(isReceive);
        }

    }
}
