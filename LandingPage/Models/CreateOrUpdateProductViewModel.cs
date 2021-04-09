using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LandingPage.Models
{
    public class CreateOrUpdateProductViewModel
    {
        public int? Id { get; set; }
        public string ProductCode { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Content { get; set; }
        //public string Tag { get; set; }
        public bool Status { get; set; }
        public string MetaKeyWord { get; set; }
        public string MetaDescription { get; set; }
        public string MetaTitle { get; set; }
        public string ParentCode { get; set; }
        public string MainImageUrl { get; set; }
        public int ProductCategoryId { get; set; }
        public List<string> SubImagesUrl { get; set; }
        public SelectList ListCategory { get; set; }
    }
}
