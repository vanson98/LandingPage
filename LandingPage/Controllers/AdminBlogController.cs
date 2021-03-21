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
            var data = await _blogsService.GetAll();
            var listBlog = data.Select(b => new BlogViewModel()
            {
                Id = b.Id,
                BlogTitle = b.BlogTitle
            }).ToList();
            return View("~/Views/Admin/Blogs/Index.cshtml",listBlog);
        }

        public async Task<IActionResult> CreateItem()
        {
            var listBlogCategory = await _blogsService.GetAllBlogCategory();
            ViewBag.BlogCategories = listBlogCategory;
            return View("~/Views/Admin/Blogs/CreateItem.cshtml");
        }


        [HttpPost]
        public async Task<IActionResult> SaveBlog([FromBody]CreateBlogInputDto input)
        {
            var userId = User.Claims.FirstOrDefault(c => c.Type == "Id").Value;
            input.CreateUserId = userId;
            var result = await _blogsService.CreateBlog(input);
            if (result > 0)
            {
                return Json(new { StatusCode = 200, Message = "Success" });
            }
            return Json(new { StatusCode = 500, Message = "Error" });
        }
    }
}
