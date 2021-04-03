using System;
using System.Collections.Generic;
using System.Text;

namespace LandingPage.Service.Dto.Product
{
    public class ProductDto
    {
        public int? Id { get; set; }
        public string ProductCode { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Content { get; set; }
        public bool Status { get; set; }
        public string MetaKeyWord { get; set; }
        public string MetaDescription { get; set; }
        public string MetaTitle { get; set; }
        public string ParentCode { get; set; }
        public List<ProductImageDto> ListImage { get; set; }
        public int ProductCategoryId { get; set; }
        public string PorductCategoryName { get; set; }
    }
}
