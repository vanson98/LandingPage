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
        private IProductCategoryService _productCategoryService;

        public AdminProductController(IProductService productService, IProductCategoryService productCategoryService)
        {
            _productService = productService;
            _productCategoryService = productCategoryService;
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

        public IActionResult CreateOrUpdate([FromQuery]int? productId)
        {
            var listProductCategory = _productCategoryService.GetAll();
            var listParentProduct = _productService.GetAllParentProduct();
            ViewBag.ListProductCategory = listProductCategory;
            ViewBag.ListParentProduct = listParentProduct;
            CreateOrUpdateProductViewModel model = null;
            if (productId != null && productId!=0)
            {
                var prod = _productService.GetById(productId.Value);
                model = new CreateOrUpdateProductViewModel()
                {
                    Id = prod.Id,
                    Content = prod.Content,
                    MetaDescription = prod.MetaDescription,
                    MetaKeyWord = prod.MetaKeyWord,
                    MetaTitle = prod.MetaTitle,
                    Status = prod.Status,
                    Description = prod.Description,
                    UrlMainImage = null,
                    LinkImages = new string[0],
                    Name = prod.Name,
                    ParentId = prod.ParentId,
                    ProductCategoryId = prod.ProductCategoryId,
                    ProductCode = prod.ProductCode
                };
            }
            else
            {
                model = new CreateOrUpdateProductViewModel();
            }
            return View("~/Views/Admin/Products/CreateOrUpdateItem.cshtml", model);
        }
    }
}
