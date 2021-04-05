using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LandingPage.Models
{
    public class DetailProductViewModel
    {
        public string MainImageBase64 { get; set; }
        public List<string> SubImagesBase64 { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string CategoryName { get; set; }
        public string Content { get; set; }
        public List<ExhibitProductViewModel> ListSubProduct { get; set; }
    }
}
