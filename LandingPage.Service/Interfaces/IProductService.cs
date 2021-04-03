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
        List<ParentProductSelectDto> GetAllParentProduct(int? productId,string mode);
        bool Add(ProductDto entity, Guid createUserId);
        bool Update(ProductDto entity);
        bool Delete(int Id);
        Task<string> GetMainImageOfProduct(int productId);
        Task<List<string>> GetListSubImageOfProduct(int productId);
        bool ChangeStatus(int productId);
        List<ExhibitProductDto> GetAllProductForExhibit();
    }
}
