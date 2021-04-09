using LadingPage.Common.Utility;
using LandingPage.Models;
using LandingPage.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LandingPage.Controllers
{
    public class EximaniBlogController : Controller
    {
        private readonly IBlogsService _blogService;

        public EximaniBlogController(IBlogsService blogService)
        {
            _blogService = blogService;
        }

        public async Task<IActionResult> Index()
        {
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
            var blogDetail =  new ExhibitDetailBlogViewModel()
            {
                Content = blog.Content,
                Title = blog.Title,
                MetaDescription = blog.MetaDescription,
                MetaKeyWord = blog.MetaKeyWord,
                MetaTitle = blog.MetaTitle
            };
            return View(blogDetail);
        }
    }
}
