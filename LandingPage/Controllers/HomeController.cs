using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using LandingPage.Models;
using LandingPage.Service.Interfaces;
using LadingPage.Common.Utility;
using Microsoft.Extensions.Configuration;

namespace LandingPage.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private IProductService _productService;
        private IConfiguration Configuration;

        public HomeController(ILogger<HomeController> logger, 
            IProductService productService, 
            IConfiguration configuration)
        {
            _logger = logger;
            _productService = productService;
            Configuration = configuration;
        }

        public IActionResult Index()
        { 
            ViewBag.Title = Configuration["SeoConfig:Home:Title"];
            ViewBag.KeyWords = Configuration["SeoConfig:Home:KeyWords"];
            ViewBag.Descriptions = Configuration["SeoConfig:Home:Description"];
            var listExProdCategory = new List<ExhibitProductCategoryViewModel>();
            var listProductCategory = _productService.GetAllProductByCategoryOnView();
            foreach (var pc in listProductCategory)
            {
                var exProdCate = new ExhibitProductCategoryViewModel()
                {
                    CategoryId = pc.CategoryId,
                    CategoryName = pc.CategoryName,
                    ListExhibitProduct = pc.ListExhibitProduct.Select(p => new ExhibitProductViewModel()
                    {
                        UrlMainImage = p.UrlMainImage,
                        LinkDetailProduct = Url.Action("Detail", "EximaniProduct", new { name = p.ProductName.GetSeoName() + "-" + p.ProductId }),
                        ProductName = p.ProductName
                    }).ToArray()
                };
                listExProdCategory.Add(exProdCate);
            }
            return View(listExProdCategory);
        }
       
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
