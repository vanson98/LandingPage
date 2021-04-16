using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LandingPage.Models;
using LandingPage.Service.Dto.Product;
using LandingPage.Service.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace LandingPage.Controllers
{
    [Authorize]
    [DisableRequestSizeLimit]
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
            ViewBag.Title = "Product Manager";
            var data = _productService.GetAll();
            var listProduct = data.Select(p => new ProductViewModel()
            {
                Id = p.Id,
                Name = p.Name,
                ProductCode = p.ProductCode,
                Status = p.Status,
                ProductCategoryName = p.PorductCategoryName
            }).ToList();
            return View("~/Views/Admin/Products/Index.cshtml", listProduct);
        }

        public async Task<IActionResult> CreateOrUpdate([FromQuery]int? productId)
        {
            ViewBag.Title = "Create Or Update Product";
            var listProductCategory = _productCategoryService.GetAll(null);
           
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
                    Name = prod.Name,
                    ProductCategoryId = prod.ProductCategoryId,
                    ProductCode = prod.ProductCode, 
                };
                model.ListCategory = new SelectList(listProductCategory, "Id", "Name", prod.ProductCategoryId);
                model.SubImagesUrl = await _productService.GetListSubImageOfProduct(productId.Value,false);
                model.MainImageUrl = _productService.GetMainImageOfProduct(productId.Value);
            }
            else
            {
                model = new CreateOrUpdateProductViewModel();
                model.ListCategory = new SelectList(listProductCategory, "Id", "Name");
                model.SubImagesUrl = new List<string>();
            }
            return View("~/Views/Admin/Products/CreateOrUpdateItem.cshtml", model);
        }

        [HttpPost]
        public IActionResult SaveProduct([FromBody]ProductDto input)
        {
            var userId = User.Claims.FirstOrDefault(c => c.Type == "Id").Value;
            var result = _productService.Add(input, Guid.Parse(userId));
            if (result == true)
            {
                return Json(new { StatusCode = 202, Message = "Success", UrlRedirect = Url.Action("Index", "AdminProduct") });
            }
            return Json(new { StatusCode = 500, Message = "Error" });
        }

        [HttpPost]
        public IActionResult UpdateProduct([FromBody]ProductDto input)
        {
            var userId = User.Claims.FirstOrDefault(c => c.Type == "Id").Value;
            var result = _productService.Update(input);
            if (result == true)
            {
                return Json(new { StatusCode = 202, Message = "Success", UrlRedirect = Url.Action("Index", "AdminProduct") });
            }
            return Json(new { StatusCode = 500, Message = "Error" });
        }

        public IActionResult ChangeStatus([FromQuery]int productId)
        {
            var result = _productService.ChangeStatus(productId);
            if (result == true)
            {
                return RedirectToAction("Index", "AdminProduct");
            }
            return Json(new { StatusCode = 500, Message = "Error" });
        }

        public IActionResult Delete([FromQuery]int productId)
        {
            var result = _productService.Delete(productId);
            if (result == true)
            {
                return RedirectToAction("Index", "AdminProduct");
            }
            return Json(new { StatusCode = 500, Message = "Error" });
        }

    }
}
