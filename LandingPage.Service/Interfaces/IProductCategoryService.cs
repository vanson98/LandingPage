using LandingPage.Service.Dto.ProductCategory;
using System;
using System.Collections.Generic;
using System.Text;

namespace LandingPage.Service.Interfaces
{
    public interface IProductCategoryService
    {
        List<ProductCategoryDto> GetAll();
    }
}
