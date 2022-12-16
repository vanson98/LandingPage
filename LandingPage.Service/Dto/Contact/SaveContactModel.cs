using LadingPage.Common.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace LandingPage.Service.Dto.Contact
{
    public class SaveContactModel
    {
        public string FullName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public int TicketType { get; set; }
        public int TicketAmount { get; set; }
        public string Address { get; set; }
        public string Question { get; set; }
        public DateTime? CreatedDate { get; set; }
    }
}
