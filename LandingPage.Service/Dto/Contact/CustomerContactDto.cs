using System;
using System.Collections.Generic;
using System.Text;

namespace LandingPage.Service.Dto.Contact
{
    public class CustomerContactDto
    {
        public int? Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName { get; set; }
        public string Message { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime? CreatedDate { get; set; }
    }
}
