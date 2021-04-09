using LadingPage.Common.Utility;
using LandingPage.Domain.EF;
using LandingPage.Domain.Entities;
using LandingPage.Service.Dto.Product;
using LandingPage.Service.Interfaces;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LandingPage.Service.Services
{
    public class ProductService : IProductService
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IHostingEnvironment _hostingEnvironment;

        public ProductService(ApplicationDbContext dbContext, IHostingEnvironment hostingEnvironment)
        {
            _dbContext = dbContext;
            _hostingEnvironment = hostingEnvironment;
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
                var productSeoName = product.Name.GetSeoName();
                SaveProductImage(product.Id, productSeoName, entity.ListImage);
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

        public List<ExhibitProductCategoryDto> GetAllProductByCategoryOnView()
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
                        UrlMainImage = GetMainImageOfProduct(p.Id),
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
            var product = (from p in _dbContext.Products.Select(p=>p)
                        where p.Id == id
                        select new ProductDto()
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
            return product;
        }

        public async Task<List<string>> GetListSubImageOfProduct(int productId, bool isGetAll)
        {
            return await _dbContext.Set<ProductImage>()
                    .Where(pi => pi.ProductId == productId && (isGetAll || !pi.IsMainImage))
                    .Select(pi => @"/eximani-product-images/"+pi.Url).ToListAsync();
        }

        public string GetMainImageOfProduct(int productId)
        {
            return _dbContext.Set<ProductImage>()
                   .Where(pi => pi.ProductId == productId && pi.IsMainImage)
                   .Select(pi => @"/eximani-product-images/" + pi.Url).FirstOrDefault();
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
                var productSeoName = product.Name.GetSeoName();
                RemoveAllProductImage(product.Id);
                SaveProductImage(product.Id, productSeoName, entity.ListImage);
                product.Content = entity.Content;
                product.Description = entity.Description;
                product.MetaDescription = entity.MetaDescription;
                product.MetaKeyWord = entity.MetaKeyWord;
                product.MetaTitle = entity.MetaTitle;
                product.Name = entity.Name;
                product.ProductCategoryId = entity.ProductCategoryId;
                product.Status = entity.Status;
                _dbContext.Update<Product>(product);
                _dbContext.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }

        }
        private void RemoveAllProductImage(int productId)
        {
            var productImages = _dbContext.ProductImages.Where(pi => pi.ProductId == productId).Select(pi => pi).ToList();
            // Xóa toàn bộ ảnh cũ 
            foreach (var pi in productImages)
            {
                if (pi.Url != null)
                {
                    var fullPath = Path.Combine(_hostingEnvironment.WebRootPath, @"eximani-product-images", pi.Url);
                    if (File.Exists(fullPath))
                    {
                        File.Delete(fullPath);
                    }
                }
            }
            _dbContext.ProductImages.RemoveRange(productImages);
            _dbContext.SaveChanges();
        }

        private void SaveProductImage(int productId, string productSeoName, List<ProductImageDto> listImage)
        {
            foreach (var item in listImage)
            {
                var productImage = new ProductImage()
                {
                    Url = null,
                    IsMainImage = item.IsMainImage,
                    ProductId = productId
                };
                _dbContext.ProductImages.Add(productImage);
                _dbContext.SaveChanges();
                productImage.Url = ConvertBase64ToFile(item.Base64, productImage.Id, productSeoName);
                _dbContext.ProductImages.Update(productImage);
                _dbContext.SaveChanges();
            };
        }

        private string ConvertBase64ToFile(string base64Data, int productId, string productSeoName)
        {
            var base64Format = base64Data.Split(",")[1];
            var extensionEncode = base64Format.Substring(0, 5);
            string extensionFile = null;
            switch (extensionEncode)
            {
                case "iVBOR":
                    extensionFile = ".png";
                    break;
                case "/9j/4":
                    extensionFile = ".jpg";
                    break;
                default:
                    break;
            }
            // Chuyển base 64 sang byte
            byte[] bytes = Convert.FromBase64String(base64Format);
            var fileName = productSeoName + "-" + productId + extensionFile;
            var folderName = @"eximani-product-images";
            var pathFolder = Path.Combine(_hostingEnvironment.WebRootPath, folderName);
            // Tạo mới folder (nếu có rồi sẽ tự động bỏ qua)
            Directory.CreateDirectory(pathFolder);
            // Lưu vào thư mục
            var fullPath = Path.Combine(_hostingEnvironment.WebRootPath, folderName, fileName);
            File.WriteAllBytes(fullPath, bytes);
            return fileName;
        }
    }
}
