using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using LandingPage.Models;
using Microsoft.AspNetCore.Authorization;
using LandingPage.Service.Interfaces;
using LadingPage.Common.Utility;

namespace LandingPage.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private IProductService _productService;

        public HomeController(ILogger<HomeController> logger, IProductService productService)
        {
            _logger = logger;
            _productService = productService;
        }

        public IActionResult Index()
        {
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
                        LinkDetailProduct = Url.Action("Detail", "EximaniProduct", new { name = p.ProductName.GetSeoName() }),
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
