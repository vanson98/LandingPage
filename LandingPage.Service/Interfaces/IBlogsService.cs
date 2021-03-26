﻿using LandingPage.Service.Dto.Blog;
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
         Task<List<BlogCategoryDto>> GetAllBlogCategory();
    }
}
