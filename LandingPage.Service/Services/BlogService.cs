using LandingPage.Domain.EF;
using LandingPage.Domain.Entities;
using LandingPage.Service.Dto.Blog;
using LandingPage.Service.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LandingPage.Service.Services
{
    public class BlogService : IBlogsService
    {
        private readonly ApplicationDbContext _dbContext;

        public BlogService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Task<int> CreateBlog(CreateBlogInputDto input)
        {
            var blog = new Blog()
            {
                BlogCategoryId = input.BlogCategoryId,
                Content = input.Content,
                CreateUserId = Guid.Parse(input.CreateUserId),
                CreatedDate = DateTime.Now,
                Title = input.Title,
                UrlImage = input.UrlImage,
                Published = input.Published,
                ShortDescription = input.ShortDescription,
                MetaTitle = input.MetaTitle,
                MetaDescription = input.MetaDescription,
                MetaKeyWord = input.MetaKeyWord
            };
            _dbContext.AddAsync(blog);
            return _dbContext.SaveChangesAsync();
        }

        public Task<List<BlogDto>> GetAll()
        {
            var listBlog = _dbContext.Set<Blog>().Where(c => c.IsDeleted == false).Select(b => new BlogDto()
            {
                Id = b.Id,
                BlogTitle = b.Title
            }).ToListAsync();
            return listBlog;
        }

        public Task<List<BlogCategoryDto>> GetAllBlogCategory()
        {
            var listBlogCategory = _dbContext.Set<BlogCategory>().Where(c=>c.IsDeleted==false).Select(c=>new BlogCategoryDto() { 
                Id = c.Id,
                Name = c.Name
            }).ToListAsync();
            return listBlogCategory;
        }
    }
}
