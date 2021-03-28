using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LandingPage.Models
{
    public class BlogViewModel
    {
        public int Id { get; set; }
        public string BlogTitle { get; set; }
        public string Author { get; set; }
        public string CreatedDate { get; set; }
        public string Category { get; set; }
        public bool Published { get; set; }
    }
}
