using LandingPage.Domain.EF;
using LandingPage.Domain.Entities;
using LandingPage.Service.Dto.Product;
using LandingPage.Service.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LandingPage.Service.Services
{
    public class ProductService : IProductService
    {
        private readonly ApplicationDbContext _dbContext;

        public ProductService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public bool Add(ProductDto entity, Guid createUserId)
        {
            try
            {
                var product = new Product()
                {
                    Content = entity.Content,
                    CreatedDate = DateTime.Now,
                    CreateUserId = createUserId,
                    Description = entity.Description,
                    MetaDescription = entity.MetaDescription,
                    MetaKeyWord = entity.MetaKeyWord,
                    MetaTitle = entity.MetaTitle,
                    Name = entity.Name,
                    ProductCategoryId = entity.ProductCategoryId,
                    ProductCode = entity.ProductCode,
                    Status = entity.Status,
                    ParentCode = entity.ParentCode
                };
                _dbContext.Add<Product>(product);
                _dbContext.SaveChanges();
                foreach (var image in entity.ListImage)
                {
                    var productImage = new ProductImage()
                    {
                        Base64 = image.Base64,
                        IsMainImage = image.IsMainImage,
                        ProductId = product.Id
                    };
                    _dbContext.Add<ProductImage>(productImage);
                }
                _dbContext.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }

        }

        public bool ChangeStatus(int productId)
        {
            try
            {
                var product = _dbContext.Set<Product>().Find(productId);
                product.Status = !product.Status;
                _dbContext.Update<Product>(product);
                _dbContext.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }

        }

        public bool Delete(int id)
        {
            try
            {
                var product = _dbContext.Find<Product>(id);
                if (product == null)
                {
                    return false;
                }
                product.IsDeleted = true;
                // Lấy toàn bộ sản phẩm con
                var listSubProduct = _dbContext.Set<Product>().Where(p => p.ParentCode == product.ProductCode);
                foreach (var subProduct in listSubProduct)
                {
                    subProduct.IsDeleted = true;
                    _dbContext.Update<Product>(subProduct);
                }
                _dbContext.Update<Product>(product);
                _dbContext.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }

        }

        public List<ProductDto> GetAll()
        {
            var listProduct = _dbContext.Set<Product>().Where(p => p.IsDeleted == false).Join(
                _dbContext.Set<ProductCategory>(),
                p => p.ProductCategoryId,
                pc => pc.Id,
                (p, pc) => new ProductDto
                {
                    Id = p.Id,
                    ProductCode = p.ProductCode,
                    Name = p.Name,
                    Status = p.Status,
                    ParentCode = p.ParentCode,
                    ProductCategoryId = p.ProductCategoryId,
                    PorductCategoryName = pc.Name
                }).ToList();
            return listProduct;
        }

        public List<ParentProductSelectDto> GetAllParentProduct(int? productId, string mode)
        {

            // Lúc tạo mới
            if (mode == "create")
            {
                // Không lấy các parent product tắt kích hoạt
                return _dbContext.Set<Product>()
                           .Where(p => p.IsDeleted == false && p.ParentCode == null && p.Status)
                           .Select(p => new ParentProductSelectDto()
                           {
                               Code = p.ProductCode,
                               CodeName = p.ProductCode + "-" + p.Name
                           }).ToList();
            }
            else
            {
                // Lúc update
                return _dbContext.Set<Product>()
                          .Where(p => p.IsDeleted == false && p.ParentCode == null && p.Id != productId)
                          .Select(p => new ParentProductSelectDto()
                          {
                              Code = p.ProductCode,
                              CodeName = p.ProductCode + "-" + p.Name
                          }).ToList();
            }



        }

        public List<ExhibitProductDto> GetAllProductForExhibit()
        {
            var listProduct = (from p in _dbContext.Set<Product>().Select(p => p)
                               where p.IsDeleted == false && p.Status == true && p.ParentCode == null
                               select new ExhibitProductDto()
                               {
                                   Base64 = _dbContext.Set<ProductImage>().Where(pi => pi.ProductId == p.Id && pi.IsMainImage == true).FirstOrDefault().Base64,
                                   ProductId = p.Id,
                                   ProductName = p.Name
                               }).ToList();
            return listProduct;
        }

        public ProductDto GetById(int id)
        {
            return _dbContext.Set<Product>().Where(p => p.Id == id).Select(p => new ProductDto()
            {
                Id = p.Id,
                ProductCode = p.ProductCode,
                Name = p.Name,
                Description = p.Description,
                Content = p.Content,
                Status = p.Status,
                MetaDescription = p.MetaDescription,
                MetaKeyWord = p.MetaKeyWord,
                MetaTitle = p.MetaTitle,
                ProductCategoryId = p.ProductCategoryId,
                ParentCode = p.ParentCode
            }).FirstOrDefault();
        }

        public async Task<List<string>> GetListSubImageOfProduct(int productId)
        {
            return await _dbContext.Set<ProductImage>()
                    .Where(pi => pi.ProductId == productId && !pi.IsMainImage)
                    .Select(pi => pi.Base64).ToListAsync();
        }

        public async Task<string> GetMainImageOfProduct(int productId)
        {
            return await _dbContext.Set<ProductImage>()
                   .Where(pi => pi.ProductId == productId && pi.IsMainImage)
                   .Select(pi => pi.Base64).FirstOrDefaultAsync();
        }

        public bool Update(ProductDto entity)
        {
            try
            {
                var product = _dbContext.Find<Product>(entity.Id);
                if (product == null)
                {
                    return false;
                }
                product.Content = entity.Content;
                product.Description = entity.Description;
                product.MetaDescription = entity.MetaDescription;
                product.MetaKeyWord = entity.MetaKeyWord;
                product.MetaTitle = entity.MetaTitle;
                product.Name = entity.Name;
                product.ParentCode = entity.ParentCode;
                product.ProductCategoryId = entity.ProductCategoryId;
                product.Status = entity.Status;
                // Xóa hết ảnh cũ
                var listProductImage = _dbContext.Set<ProductImage>().Where(pi => pi.ProductId == entity.Id).Select(pi => pi);
                _dbContext.Set<ProductImage>().RemoveRange(listProductImage);
                // Thêm lại toàn bộ ảnh
                foreach (var image in entity.ListImage)
                {
                    var productImage = new ProductImage()
                    {
                        Base64 = image.Base64,
                        IsMainImage = image.IsMainImage,
                        ProductId = product.Id
                    };
                    _dbContext.Add<ProductImage>(productImage);
                }
                _dbContext.Update<Product>(product);
                _dbContext.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }
    }
}
