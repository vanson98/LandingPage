using LandingPage.Domain.EF;
using LandingPage.Domain.Entities;
using LandingPage.Service.Dto.Product;
using LandingPage.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LandingPage.Service.Services
{
    public class ProductService : IProductService
    {
        private readonly ApplicationDbContext _dbContext;

        public ProductService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public bool Add(CreateProductInputDto entity)
        {
            var product = new Product()
            {
                Content = entity.Content,
                CreatedDate = DateTime.Now,
                CreateUserId = entity.CreateUserId,
                Description = entity.Description,
                MetaDescription = entity.MetaDescription,
                MetaKeyWord = entity.MetaKeyWord,
                MetaTitle = entity.MetaTitle,
                Name = entity.Name,
                ParentId = entity.ParentId,
                ProductCategoryId = entity.ProductCategoryId,
                ProductCode = entity.ProductCode,
                Status = entity.Status,
                Tag = entity.Tag,
                UrlMainImage = entity.UrlMainImage
            };
            _dbContext.Add<Product>(product);
            return _dbContext.SaveChanges() > 1 ? true : false;
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
                var listSubProduct = _dbContext.Set<Product>().Where(p => p.ParentId == id);
                foreach (var subProduct in listSubProduct)
                {
                    subProduct.IsDeleted = true;
                    _dbContext.Update<Product>(subProduct);
                }
                _dbContext.Update<Product>(product);
                return _dbContext.SaveChanges() > 1 ? true : false;
            }
            catch (Exception e)
            {
                return false;
            }

        }

        public List<ProductDto> GetAll()
        {
            return _dbContext.Set<Product>().Join(
                _dbContext.Set<ProductCategory>(),
                p => p.ProductCategoryId,
                pc => pc.Id,
                (p, pc) => new ProductDto
                {
                    Id = p.Id,
                    ProductCode = p.ProductCode,
                    Name = p.Name,
                    Status = p.Status,
                    ParentCode = p.ParentId == 0 ? "" : GetParentProductCode(p.Id),
                    UrlMainImage = p.UrlMainImage,
                    ProductCategoryId = p.ProductCategoryId,
                    PorductCategoryName = pc.Name
                }).ToList();
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
                Tag = p.Tag,
                Status = p.Status,
                MetaDescription = p.MetaDescription,
                MetaKeyWord = p.MetaKeyWord,
                MetaTitle = p.MetaTitle,
                ParentId = p.ParentId,
                ImageUrls = GetAllProductImageUrl(p.Id),
                ProductCategoryId = p.ProductCategoryId,
                UrlMainImage = p.UrlMainImage
            }).FirstOrDefault();
        }

        public bool Update(ProductDto entity)
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
            product.ParentId = entity.ParentId;
            product.ProductCategoryId = entity.ProductCategoryId;
            product.ProductCode = entity.ProductCode;
            product.Status = entity.Status;
            product.Tag = entity.Tag;
            product.UrlMainImage = entity.UrlMainImage;
            // Update list ảnh ...
            _dbContext.Update<Product>(product);
            return _dbContext.SaveChanges() > 1 ? true : false;
        }

        private string GetParentProductCode(int id)
        {
            return _dbContext.Set<Product>().Find(id).ProductCode;
        }

        private string[] GetAllProductImageUrl(int productId)
        {
            return _dbContext.ProductImages.Where(pi => pi.ProductId == productId).Select(pi => pi.UrlImage).ToArray();
        }
    }
}
