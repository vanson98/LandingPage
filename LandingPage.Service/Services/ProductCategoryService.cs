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

        public bool Add(ProductCategoryDto input,Guid creatUserId)
        {
            var pc = new ProductCategory()
            {
                Name = input.Name,
                Description = input.Description,
                CreateUserId = creatUserId,
                CreatedDate = DateTime.Now,
            };
            _dbContext.ProductCategories.Add(pc);
            var result = _dbContext.SaveChanges();
            if (result > 0)
            {
                return true;
            }
            return false;
        }

        public bool Delete(int id)
        {
            var pc = _dbContext.ProductCategories.Find(id);
            pc.IsDeleted = true;
            _dbContext.ProductCategories.Update(pc);
            return _dbContext.SaveChanges() > 0 ? true : false;
        }

        public ProductCategoryDto Get(int id)
        {
            return _dbContext.ProductCategories.Where(pc => pc.Id == id).Select(pc => new ProductCategoryDto()
            {
                Id = pc.Id,
                Name = pc.Name,
                Description = pc.Description
            }).FirstOrDefault();
        }

        public List<ProductCategoryDto> GetAll(string searchText)
        {
            return _dbContext.ProductCategories
                .Where(pc =>
                    searchText == "" || 
                    searchText == null ||
                    pc.Name.Contains(searchText) || 
                    pc.Description.Contains(searchText))
                .Where(pc=>pc.IsDeleted==false)
                .Select(pc => new ProductCategoryDto()
                {
                    Description = pc.Description,
                    Id = pc.Id,
                    Name = pc.Name
                }).ToList();
        }

        public bool Update(ProductCategoryDto input)
        {
            var pc = _dbContext.ProductCategories.Find(input.Id);
            pc.Name = input.Name;
            pc.Description = input.Description;
            _dbContext.ProductCategories.Update(pc);
            return _dbContext.SaveChanges() > 0 ? true : false;
        }
    }
}
