using System;
using System.Collections.Generic;
using System.Text;

namespace LandingPage.Service.Dto.Blog
{
    public class BlogDto
    {
        public int Id { get; set; }
        public string BlogTitle { get; set; }
        public string Author { get; set; }
        public string CreatedDate { get; set; }
        public string CategoryName { get; set; }
        public bool Published { get; set; }
    }
}
