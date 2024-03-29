﻿using LadingPage.Common.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace LandingPage.Service.Dto.Contact
{
    public class CustomerContactDto
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string TicketType { get; set; }
        public int TicketAmount { get; set; }
        public string Address { get; set; }
        public string Question { get; set; }
        public string CreatedDate { get; set; }
    }
}
