﻿using System;
using System.Collections.Generic;
using System.Text;

namespace LandingPage.Domain.Models
{
     public class FeedBack : BaseModel
    {
        public string AvatarUrl { get; set; }
        public string ClientName { get; set; }
        public string Country { get; set; }
        public string content { get; set; }
    }
}
