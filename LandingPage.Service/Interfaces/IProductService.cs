using LandingPage.Service.Dto.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LandingPage.Service.Interfaces
{
    public interface IProductService
    {
        ProductDto GetById(int id);
        List<ProductDto> GetAll();
        bool Add(ProductDto entity, Guid createUserId);
        bool Update(ProductDto entity);
        bool Delete(int Id);
        string GetMainImageOfProduct(int productId);
        Task<List<string>> GetListSubImageOfProduct(int productId,bool isGetAll);
        bool ChangeStatus(int productId);
        List<ExhibitProductCategoryDto> GetAllProductByCategoryOnView();
        ProductDto GetProductDetailById(int? productId,string seoName);
    }
}
