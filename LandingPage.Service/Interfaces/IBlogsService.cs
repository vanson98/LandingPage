using LandingPage.Service.Dto.Blog;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LandingPage.Service.Interfaces
{
    public interface IBlogsService
    {
        public Task<List<BlogDto>> GetAll();
        public Task<int> CreateBlog(CreateBlogInputDto input);
        public Task<List<BlogCategoryDto>> GetAllBlogCategory();
    }
}
