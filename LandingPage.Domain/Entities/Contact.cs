using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LandingPage.Domain.Entities
{
    public class ContactModel : BaseModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Message { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
    }
}
