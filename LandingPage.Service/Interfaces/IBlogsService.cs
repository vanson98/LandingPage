using LandingPage.Service.Dto.Blog;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LandingPage.Service.Interfaces
{
    public interface IBlogsService
    {
        Task<List<BlogDto>> GetAll();
        Task<int> CreateBlog(CreateBlogInputDto input);
        Task<BlogDto> GetById(int id);
        Task<int> UpdateBlog(BlogDto input);
        Task<int> Delete(int blogId);
        Task<List<BlogDto>> GetAllOnView();
        Task<BlogDto> GetDetailBlog(int id);
    }
}
