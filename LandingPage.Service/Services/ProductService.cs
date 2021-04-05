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
                    Status = entity.Status
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
                    ProductCategoryId = p.ProductCategoryId,
                    PorductCategoryName = pc.Name
                }).ToList();
            return listProduct;
        }

        public List<ExhibitProductCategoryDto> GetAllProductCategoryOnView()
        {
            var groupProducts = (from p in _dbContext.Set<Product>().Select(p => p).ToList()
                               join pc in _dbContext.Set<ProductCategory>().Select(pc => pc).ToList() on p.ProductCategoryId equals pc.Id
                               where p.IsDeleted == false && p.Status == true
                               group p by pc into gp
                               select gp);
            var listExProductCategory = new List<ExhibitProductCategoryDto>();
            foreach (var gp in groupProducts)
            {
                var exProductCategory = new ExhibitProductCategoryDto()
                {
                    CategoryId = gp.Key.Id,
                    CategoryName = gp.Key.Name,
                    ListExhibitProduct = gp.Select(p => new ExhibitProductDto()
                    {
                        Base64 = _dbContext.Set<ProductImage>().Where(pi => pi.ProductId == p.Id && pi.IsMainImage == true).FirstOrDefault()?.Base64,
                        ProductId = p.Id,
                        ProductName = p.Name
                    }).ToList()
                };
                listExProductCategory.Add(exProductCategory);
            }
            return listExProductCategory;
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
                ProductCategoryId = p.ProductCategoryId
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

        public ProductDto GetProductDetailById(int productId)
        {
            var productDetail = (from p in  _dbContext.Set<Product>().Select(p=>p).ToList()
                                join pc in _dbContext.Set<ProductCategory>().Select(p => p).ToList() on p.ProductCategoryId equals pc.Id
                                where p.Id == productId
                                select new ProductDto(){
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
                                        PorductCategoryName = pc.Name
                                }).FirstOrDefault();
            return productDetail;
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
