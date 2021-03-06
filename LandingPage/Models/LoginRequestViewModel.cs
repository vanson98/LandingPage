using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LandingPage.Models
{
    public class LoginRequestViewModel
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public bool RememberMe { get; set; }
    }
}
