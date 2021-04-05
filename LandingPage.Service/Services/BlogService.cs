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
                //BlogCategoryId = input.BlogCategoryId,  
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

        public async Task<int> Delete(int blogId)
        {
            var blog = _dbContext.Find<Blog>(blogId);
            if (blog == null)
            {
                return -1;
            }
            blog.IsDeleted = true;
            _dbContext.Update<Blog>(blog);
            return await _dbContext.SaveChangesAsync();
        }

        public async Task<List<BlogDto>> GetAll()
        {
            var listBlog = await (from b in _dbContext.Set<Blog>().Select(b=>b)
                           join u in _dbContext.Set<AppUser>().Select(u=>u) on b.CreateUserId equals u.Id
                           where b.IsDeleted == false 
                           select new BlogDto()
                           {
                               Id = b.Id,
                               Title = b.Title,
                               Author = u.FirstName+" "+u.LastName,
                               CreatedDate = b.CreatedDate.Value.ToString("dd/MM/yyyy"),
                               Published = b.Published
                           }).ToListAsync();
            return listBlog;
        }

        public async Task<List<BlogDto>> GetAllOnView()
        {
            var listBlog = await _dbContext.Set<Blog>().Where(b => b.IsDeleted == false && b.Published == true)
                                    .Select(b => new BlogDto()
                                    {
                                        Id = b.Id,
                                        MetaDescription = b.MetaDescription,
                                        MetaKeyWord = b.MetaKeyWord,
                                        MetaTitle = b.MetaTitle,
                                        ShortDescription = b.ShortDescription,
                                        Title = b.Title
                                    }).ToListAsync();
            return listBlog;
        }

        public async Task<BlogDto> GetById(int id)
        {
            return await _dbContext.Set<Blog>().Where(b=>b.Id==id).Select(b=>new BlogDto() {
                Id = b.Id,
                Title = b.Title,
                Published = b.Published,
                Content = b.Content,
                ShortDescription = b.ShortDescription,
                MetaDescription = b.MetaDescription,
                MetaKeyWord = b.MetaKeyWord,
                MetaTitle = b.MetaTitle,
                UrlImage = b.UrlImage
            }).FirstOrDefaultAsync();
        }

        public async Task<BlogDto> GetDetailBlog(int id)
        {
            var blog = await _dbContext.Set<Blog>().Where(b => b.Id == id).Select(b => new BlogDto
            {
                Title = b.Title,
                Content = b.Content,
                MetaDescription = b.MetaDescription,
                MetaTitle = b.MetaTitle,
                MetaKeyWord = b.MetaKeyWord
            }).FirstOrDefaultAsync();
            return blog;
        }

        public async Task<int> UpdateBlog(BlogDto input)
        {
            var blog = _dbContext.Set<Blog>().Find(input.Id);
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
