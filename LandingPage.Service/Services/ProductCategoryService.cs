using LandingPage.Domain.EF;
using LandingPage.Domain.Entities;
using LandingPage.Service.Dto.ProductCategory;
using LandingPage.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LandingPage.Service.Services
{
    public class ProductCategoryService : IProductCategoryService
    {
        private readonly ApplicationDbContext _dbContext;

        public ProductCategoryService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public List<ProductCategoryDto> GetAll()
        {
            return _dbContext.Set<ProductCategory>().Select(pc => new ProductCategoryDto()
            {
                Description = pc.Description,
                Id = pc.Id,
                Name = pc.Name
            }).ToList();
        }
    }
}
