using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LandingPage.Models;
using LandingPage.Service.Dto;
using LandingPage.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace LandingPage.Controllers
{
    public class ContactController : Controller
    {
        private readonly IContactService _contactService;

        public ContactController(IContactService contactService)
        {
            _contactService = contactService;
        }


        public async Task<IActionResult> Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ReceiveContact(CustomerContactDto contactdto)
        {
            await _contactService.SaveCustomerContact(contactdto);
            return View("Index");
        }
        public async Task<IActionResult> ContactList()
        {
            return View();
        }
    }
}
