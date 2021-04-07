using LandingPage.Service.Dto.ProductCategory;
using System;
using System.Collections.Generic;
using System.Text;

namespace LandingPage.Service.Interfaces
{
    public interface IProductCategoryService
    {
        bool Add(ProductCategoryDto input,Guid createUserId);
        bool Update(ProductCategoryDto input);
        bool Delete(int id);
        List<ProductCategoryDto> GetAll(string searchText);
        ProductCategoryDto Get(int id);
    }
}
