using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LandingPage.Models;
using LandingPage.Service.Dto;
using LandingPage.Service.Dto.Contact;
using LandingPage.Service.Interfaces;
using LandingPage.Service.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LandingPage.Controllers
{
    [Authorize]
    public class AdminContactController : Controller
    {
        private readonly IContactService _contactService;
        public AdminContactController(IContactService contactService)
        {
            _contactService = contactService;
        }

        public async Task<IActionResult> ContactList([FromQuery]string SearchText)
        {
            ViewBag.Title = "Contact Manager";
            var listContact = await _contactService.GetAll(SearchText);
            ViewBag.SearchText = SearchText;
           
            return View("~/Views/Admin/Contacts/Index.cshtml", listContact);
        }

        public async Task<IActionResult> Detail([FromQuery]int contactId)
        {
            var contact = await _contactService.GetById(contactId);
            return Json(contact);
        }
        public IActionResult Delete([FromQuery]int id)
        {
            try
            {
                var rerult = _contactService.Delete(id);
                if (rerult)
                {
                    return Json(new { Status = 200, Message = "Delete Success" });
                }
                else
                {
                    return Json(new { Status = 500, Message = "Delete Error" });
                }
            }
            catch (Exception)
            {

                return Json(new { Status = 500, Message = "Delete Error" });
            }
        }
        public IActionResult Add([FromForm] SaveContactModel input)
        {
            var result = _contactService.SaveCustomerContact(input);
            return Json(result);    
        }
    }
}