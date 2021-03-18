using LandingPage.Domain.EF;
using LandingPage.Domain.Entities;
using LandingPage.Service.Dto.Blog;
using LandingPage.Service.Interfaces;
using System;
using System.Collections.Generic;
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
    }
}
