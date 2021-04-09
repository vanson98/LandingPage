using System;
using System.Collections.Generic;
using System.Text;

namespace LandingPage.Service.Dto.User
{
    public class UserDto
    {
        public Guid Id { get; set; }
        public string FullName { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime Dob { get; set; }
    }
}
