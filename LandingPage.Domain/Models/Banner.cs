using System;
using System.Collections.Generic;
using System.Text;

namespace LandingPage.Domain.Models
{
    public class Banner : BaseModel
    {
        public string Title { get; set; }
        public string Slogun { get; set; }
        public string SubTitle { get; set; }
        public string UrlImage { get; set; }
        public string Link { get; set; }



    }
}
