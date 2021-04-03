using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LandingPage.Models
{
    public class ProductViewModel
    {
        public int? Id { get; set; }
        public string ProductCode { get; set; }
        public string Name { get; set; }
        public bool Status { get; set; }
        public string ParentCode { get; set; } 
        public string ProductCategoryName { get; set; }
    }
}
