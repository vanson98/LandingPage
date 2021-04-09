using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LandingPage.EmailService;
using LandingPage.Models;
using LandingPage.Service.Dto.Contact;
using LandingPage.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace LandingPage.Controllers
{
    public class EximaniContactController : Controller
    {
        private readonly IContactService _contactService;
        private readonly IEmailSender _emailSender;

        public EximaniContactController(IContactService contactService, IEmailSender emailSender)
        {
            _contactService = contactService;
            _emailSender = emailSender;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ReceiveContact(CustomerContactDto contactdto)
        {
            var customerContact = await _contactService.SaveCustomerContact(contactdto);
            var mailContent = $@"
                            <div><h4>Contact Information</h4></div>
                            <div><strong>ContactId: </strong>{customerContact.Id}</div>
                            <div><strong>Customer Name: </strong>{customerContact.FirstName} {customerContact.LastName}</div>
                            <div><strong>Email: </strong>{customerContact.Email}</div>
                            <div><strong>PhoneNumber: </strong>{customerContact.PhoneNumber}</div>
                            <div><strong>Message: </strong>{customerContact.Message}</div>
                            <div><strong>Sent Date: </strong>{customerContact.CreatedDate.Value.ToString("dd/MM/yyyy HH:mm:ss")}</div>
                        ";
            var message = new Message("Khách đăng kí nhận thông tin", mailContent);
            await _emailSender.SendEmailAsync(message);
            return View("Index");
        }
    }
}
