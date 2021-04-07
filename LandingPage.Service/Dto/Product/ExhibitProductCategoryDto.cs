using System;
using System.Collections.Generic;
using System.Text;

namespace LandingPage.Service.Dto.Product
{
    public class ExhibitProductCategoryDto
    {
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public List<ExhibitProductDto> ListExhibitProduct { get; set; }
    }

    public class ExhibitProductDto
    {
        public string UrlMainImage { get; set; }
        public string ProductName { get; set; }
        public int ProductId { get; set; }
    }
}
