using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LandingPage.Models;
using LandingPage.Service.Dto.User;
using LandingPage.Service.Interfaces;
using LandingPage.Service.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LandingPage.Controllers
{
    public class AdminController : Controller
    {
        private readonly IUserService _userService;

        public AdminController(IUserService userService)
        {
            _userService = userService;
        }

        [AllowAnonymous]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login([FromForm]UserLoginDto userLoginDto)
        {
            if (ModelState.IsValid)
            {
                var result = await _userService.Authenticate(userLoginDto);
                if (result)
                {
                    return RedirectToAction("BlogList", "Admin");
                }
                else
                {
                    ViewBag.Message = "Login fail!";
                    return View();
                }
            }
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> RegisterPost([FromBody]RegisterRequestDto request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var result = await _userService.Register(request);
            if (!result)
                return BadRequest("Register fail!");
            return Ok();
        }
    }
}
