using LandingPage.Service.Dto;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LandingPage.Service.Interfaces
{
    public interface IUserService
    {
        Task<bool> Authenticate(UserLoginDto userLoginDto);
        Task<bool> Register(RegisterRequestDto request);
    }
}
