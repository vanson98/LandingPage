using System;
using System.Collections.Generic;
using System.Text;

namespace LandingPage.Service.Dto.Product
{
    public class ProductImageDto
    {
        public int Id { get; set; }
        public bool IsMainImage { get; set; }
        public string Base64 { get; set; }

    }
}
