using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LandingPage.EmailService;
using LandingPage.Models;
using LandingPage.Service.Dto.Contact;
using LandingPage.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace LandingPage.Controllers
{
    public class EximaniContactController : Controller
    {
        private readonly IContactService _contactService;
        private readonly IEmailSender _emailSender;
        private IConfiguration Configuration;
        public EximaniContactController(IContactService contactService, IEmailSender emailSender, IConfiguration configuration)
        {
            _contactService = contactService;
            _emailSender = emailSender;
            Configuration = configuration;
        }

        public IActionResult Index()
        {
            ViewBag.Title = Configuration["SeoConfig:Contact:Title"];
            ViewBag.KeyWords = Configuration["SeoConfig:Contact:KeyWords"];
            ViewBag.Descriptions = Configuration["SeoConfig:Contact:Description"];
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ReceiveContact([FromBody]CustomerContactDto contactdto)
        {
            try
            {
                var customerContact = await _contactService.SaveCustomerContact(contactdto);
                //var mailContent = $@"
                //            <div><h4>Contact Information</h4></div>
                //            <div><strong>ContactId: </strong>{customerContact.Id}</div>
                //            <div><strong>Customer Name: </strong>{customerContact.FirstName} {customerContact.LastName}</div>
                //            <div><strong>Email: </strong>{customerContact.Email}</div>
                //            <div><strong>PhoneNumber: </strong>{customerContact.PhoneNumber}</div>
                //            <div><strong>Message: </strong>{customerContact.Message}</div>
                //            <div><strong>Sent Date: </strong>{customerContact.CreatedDate.Value.ToString("dd/MM/yyyy HH:mm:ss")}</div>
                //        ";
                //var message = new Message("Khách đăng kí nhận thông tin", mailContent);
                //await _emailSender.SendEmailAsync(message);
                return Json(new { Status = 200, Message = "Success" });
            }
            catch (Exception)
            {
                return Json(new { Status = 500, Message = "Error" });
            }
            
        }
    }
}
