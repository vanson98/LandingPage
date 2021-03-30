using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LandingPage.Models;
using LandingPage.Service.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LandingPage.Controllers
{
    [Authorize]
    public class AdminProductController : Controller
    {
        private IProductService _productService;

        public AdminProductController(IProductService productService)
        {
            _productService = productService;
        }

        public IActionResult Index()
        {
            var data = _productService.GetAll();
            var listProduct = data.Select(p => new ProductViewModel()
            {
                Id = p.Id,
                Name = p.Name,
                ParentCode = p.ParentCode,
                ProductCode = p.ProductCode,
                Status = p.Status,
                ProductCategoryName = p.PorductCategoryName
            }).ToList();
            return View("~/Views/Admin/Products/Index.cshtml", listProduct);
        }

        public IActionResult CreateOrUpdate()
        {

        }
    }
}
