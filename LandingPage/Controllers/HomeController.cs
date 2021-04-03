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
            var listExProd = new List<ExhibitProductViewModel>();
            var listProduct = _productService.GetAllProductForExhibit();
            foreach (var product in listProduct)
            {
                var exProd = new ExhibitProductViewModel()
                {
                    Base64 = product.Base64,
                    LinkDetailProduct = Url.Action("Detail", "EximaniProduct", new { id = product.ProductId }),
                    ProductName = product.ProductName
                };
                listExProd.Add(exProd);
            }
            return View(listExProd);
        }
       
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
