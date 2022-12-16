using LadingPage.Common.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LandingPage.Domain.Entities
{
    public class CustomerContact : BaseModel
    {
        public string FullName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public TicketType TicketType { get; set; }
        public int TicketAmount { get; set; }
        public string Address { get; set; }
        public string Question { get; set; }
    }
}
