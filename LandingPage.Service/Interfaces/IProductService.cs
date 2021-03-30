using LandingPage.Service.Dto.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LandingPage.Service.Interfaces
{
    public interface IProductService
    {
        ProductDto GetById(int id);
        List<ProductDto> GetAll();
        bool Add(CreateProductInputDto entity);
        bool Update(ProductDto entity);
        bool Delete(int Id);
    }
}
