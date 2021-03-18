using LandingPage.Service.Dto.Blog;
using LandingPage.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LandingPage.Controllers
{
    public class AdminBlogController : Controller
    {
        private IBlogsService _blogsService;

        public AdminBlogController(IBlogsService blogsService)
        {
            _blogsService = blogsService;
        }

        public IActionResult Index()
        {
            return View("~/Views/Admin/Blogs/Index.cshtml");
        }

        public IActionResult CreateItem()
        {
            
            return View("~/Views/Admin/Blogs/CreateItem.cshtml");
        }

        [HttpPost]
        public async Task<IActionResult> SaveBlog([FromBody]CreateBlogInputDto input)
        {
            input.CreateUserId = HttpContext.Session.Id;
            var result = await _blogsService.CreateBlog(input);
            if (result > 0)
            {
                return Json(new { StatusCode = 200, Message = "Success" });
            }
            return Json(new { StatusCode = 500, Message = "Error" });
        }
    }
}
