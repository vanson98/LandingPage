using System;
using System.Collections.Generic;
using System.Text;

namespace LandingPage.Domain.Entities
{
    public class FeedBack : BaseModel
    {
        public string AvatarUrl { get; set; }
        public string ClientName { get; set; }
        public string Country { get; set; }
        public string Content { get; set; }
    }
}
