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

        public async Task<List<BlogDto>> GetAll()
        {
            var listBlog = await (from b in _dbContext.Set<Blog>()
                           join bc in _dbContext.Set<BlogCategory>() on b.BlogCategoryId equals bc.Id
                           join u in _dbContext.Set<AppUser>() on b.CreateUserId equals u.Id
                           where b.IsDeleted == false && b.Published==true
                           select new BlogDto()
                           {
                               Id = b.Id,
                               BlogTitle = b.Title,
                               Author = u.FirstName+" "+u.LastName,
                               CategoryName = bc.Name,
                               CreatedDate = b.CreatedDate.Value.ToString("dd/MM/yyyy"),
                               Published = b.Published
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

        public async Task<int> UpdateBlog(UpdateBlogInputDto input)
        {
            var blog = _dbContext.Set<Blog>().Find(input.Id);
            blog.BlogCategoryId = input.BlogCategoryId;
            blog.Content = input.Content;
            blog.Title = input.Title;
            blog.ShortDescription = input.ShortDescription;
            blog.UrlImage = input.UrlImage;
            blog.Published = input.Published;
            blog.MetaKeyWord = input.MetaKeyWord;
            blog.MetaDescription = input.MetaDescription;
            blog.MetaTitle = input.MetaTitle;
            _dbContext.Update<Blog>(blog);
            return await _dbContext.SaveChangesAsync();
        }

    }
}
