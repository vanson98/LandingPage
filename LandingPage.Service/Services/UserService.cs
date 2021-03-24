using LandingPage.Domain.Entities;
using LandingPage.Service.Dto.User;
using LandingPage.Service.Interfaces;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LandingPage.Service.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;

        public UserService(SignInManager<AppUser> signInManager, UserManager<AppUser> userManager)
        {
            this._userManager = userManager;
            this._signInManager = signInManager;
        }

        public async Task<UserDto> Authenticate(UserLoginDto userLoginDto)
        {
            var user = await _userManager.FindByNameAsync(userLoginDto.Username);
            if (user==null)
                return null;
            var result = await _signInManager.PasswordSignInAsync(user, userLoginDto.Password, false, true);
            if (result.Succeeded)
            {
                return new UserDto()
                {
                    Id = user.Id,
                    Email = user.Email,
                    FirstName = user.FirstName,
                    LastName = user.LastName
                };
            }
            return null;
        }

        public async Task<bool> Register(RegisterRequestDto request)
        {
            var user = new AppUser()
            {
                Dob = request.Dob,
                FirstName = request.FirstName,
                LastName = request.LastName,
                Email = request.Email,
                PhoneNumber = request.PhoneNumber,
                UserName = request.UserName
            };
            var result = await _userManager.CreateAsync(user, request.Password);
            if (result.Succeeded) return true;
            return false;
        }

       
    }
}
