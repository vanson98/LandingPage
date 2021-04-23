using LandingPage.Models;
using LandingPage.Service.Dto.Blog;
using LandingPage.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace LandingPage.Controllers
{
    [Authorize]
    public class AdminBlogController : Controller
    {
        private IBlogsService _blogsService;

        public AdminBlogController(IBlogsService blogsService)
        {
            _blogsService = blogsService;
        }

       
        public async Task<ActionResult> Index()
        {
            ViewBag.Title = "Blog Manager";
            var data = await _blogsService.GetAll();
            var listBlog = data.Select(b => new BlogViewModel()
            {
                Id = b.Id,
                BlogTitle = b.Title,
                Author = b.Author,
                CreatedDate = b.CreatedDate,
                Published = b.Published
            }).ToList();
            return View("~/Views/Admin/Blogs/Index.cshtml",listBlog);
        }

        public async Task<IActionResult> CreateOrUpdateItem([FromQuery]int? blogId)
        {
            ViewBag.Title = "Create Or Update Blog";
            CreateOrUpdateBlogViewModel model = null ;
            if (blogId != null && blogId!=0)
            {
                var blog = await _blogsService.GetById(blogId.Value);
                model = new CreateOrUpdateBlogViewModel()
                {
                    Id = blog.Id,
                    Content = blog.Content,
                    MetaDescription = blog.MetaDescription,
                    MetaKeyWord = blog.MetaKeyWord,
                    MetaTitle = blog.MetaTitle,
                    Published = blog.Published,
                    ShortDescription = blog.ShortDescription,
                    Title = blog.Title
                };
            }
            else
            {
                model = new CreateOrUpdateBlogViewModel();
            }
            return View("~/Views/Admin/Blogs/CreateOrUpdateItem.cshtml", model);
        }

        [HttpPost]
        public async Task<IActionResult> SaveBlog([FromBody]CreateBlogInputDto input)
        {
            var userId = User.Claims.FirstOrDefault(c => c.Type == "Id").Value;
            input.CreateUserId = userId;
            var result = await _blogsService.CreateBlog(input);
            if (result > 0)
            {
                return Json(new { StatusCode = 500, Message = "Success", UrlRedirect = Url.Action("Index", "AdminBlog") });
            }
            return Json(new { StatusCode = 500, Message = "Error" });
        }

        public async Task<IActionResult> UpdateBlog([FromBody]BlogDto input)
        {
            var result = await _blogsService.UpdateBlog(input);
            if (result > 0)
            {
                return Json(new { StatusCode = 200, Message = "Success", UrlRedirect = Url.Action("Index", "AdminBlog") });
            }
            return Json(new { StatusCode = 500, Message = "Error" });
        }

        public async Task<IActionResult> DeleteBlog([FromQuery]int blogId)
        {
            var result = await _blogsService.Delete(blogId);
            if (result > 0)
            {
                return Json(new { StatusCode = 200, Message = "Success", UrlRedirect = Url.Action("Index", "AdminBlog") });
            }
            return Json(new { StatusCode = 500, Message = "Error" });
        }
    }
}
