using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using LandingPage.Models;
using LandingPage.Service.Interfaces;
using LadingPage.Common.Utility;
using Microsoft.Extensions.Configuration;
using LandingPage.Service.Dto.Contact;
using LandingPage.Service.Services;
using System.Net;
using System.Threading.Tasks;

namespace LandingPage.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private IContactService _contactService;
        private IConfiguration Configuration;

        public HomeController(ILogger<HomeController> logger, 
            IContactService contactService, 
            IConfiguration configuration)
        {
            _logger = logger;
            _contactService = contactService;
            Configuration = configuration;
        }

        public IActionResult Index()
        { 
            ViewBag.Title = Configuration["SeoConfig:Home:Title"];
            ViewBag.KeyWords = Configuration["SeoConfig:Home:KeyWords"];
            ViewBag.Descriptions = Configuration["SeoConfig:Home:Description"];
            
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ReceiveContact([FromBody]SaveContactModel contactdto)
        {
            try
            {
                if(contactdto == null)
                    return Json(new { Status = 500, Message = "Error" });
                contactdto.CreatedDate = DateTime.Now;
                var customerContact = await _contactService.SaveCustomerContact(contactdto);
               
                return Json(new { Status = 200, Message = "Success" });
            }
            catch (Exception)
            {
                return Json(new { Status = 500, Message = "Error" });
            }

        }

        [HttpPost]
        public JsonResult VerifyResponseCaptcha([FromBody] RecaptchaResponseDto input)
        {
            ReCaptcha recaptcha = new ReCaptcha();
            string url = "https://www.google.com/recaptcha/api/siteverify?secret=" + recaptcha.SecretKey + "&response=" + input.Response;
            recaptcha.Response = (new WebClient()).DownloadString(url);
            return Json(recaptcha);
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
