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
        public IActionResult ContactView(ContactModel contact)
        {
            string fistName = contact.FirstName;
            string lastName = contact.LastName;
            string message = contact.Message;
            string email = contact.Email;
            string phoneNumber = contact.PhoneNumber;
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
