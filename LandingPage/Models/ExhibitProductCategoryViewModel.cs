using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LandingPage.Models
{
    public class ExhibitProductCategoryViewModel
    {
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public ExhibitProductViewModel[] ListExhibitProduct { get; set; }
    }
    public class ExhibitProductViewModel
    {
        public string Base64 { get; set; }
        public string ProductName { get; set; }
        public string LinkDetailProduct { get; set; }
    }

}
