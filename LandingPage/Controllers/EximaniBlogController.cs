using LadingPage.Common.Utility;
using LandingPage.Models;
using LandingPage.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LandingPage.Controllers
{
    public class EximaniBlogController : Controller
    {
        private readonly IBlogsService _blogService;
        private IConfiguration Configuration;
        public EximaniBlogController(IBlogsService blogService, IConfiguration configuration)
        {
            _blogService = blogService;
            Configuration = configuration;
        }

        public async Task<IActionResult> Index()
        {
            ViewBag.Title = Configuration["SeoConfig:Blogs:Title"];
            ViewBag.KeyWords = Configuration["SeoConfig:Blogs:KeyWords"];
            ViewBag.Descriptions = Configuration["SeoConfig:Blogs:Description"];
            var listBlog = await _blogService.GetAllOnView();
            var data = listBlog.Select(b=>new ExhibitBlogViewModel() { 
                BlogTitle = b.Title,
                ShortDescription = b.ShortDescription,
                LinkDetail = Url.Action("Detail", "EximaniBlog", new { id = b.Id, name = b.Title.GetSeoName() }),
            }).ToList();
            return View(data);
        }

        public async Task<IActionResult> Detail(int id)
        {
            var blog = await _blogService.GetDetailBlog(id);
            ViewBag.Title = blog.MetaTitle;
            ViewBag.KeyWords = blog.MetaKeyWord;
            ViewBag.Descriptions = blog.MetaDescription;
            var blogDetail =  new ExhibitDetailBlogViewModel()
            {
                Content = blog.Content,
                Title = blog.Title
            };
            return View(blogDetail);
        }
    }
}
