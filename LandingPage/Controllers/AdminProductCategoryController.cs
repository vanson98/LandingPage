using LandingPage.Models;
using LandingPage.Service.Dto.ProductCategory;
using LandingPage.Service.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LandingPage.Controllers
{
    [Authorize]
    public class AdminProductCategoryController : Controller
    {
        public readonly IProductCategoryService _productCategoryService;

        public AdminProductCategoryController(IProductCategoryService productCategoryService)
        {
            _productCategoryService = productCategoryService;
        }

        public IActionResult Index(string searchText)
        {
            ViewBag.SearchText = searchText;
            var listProductCategory = _productCategoryService.GetAll(searchText).Select(pc=> new ProductCategoryViewModel() { 
                Id = pc.Id,
                Description = pc.Description,
                Name = pc.Name
            }).ToList();
            return View("~/Views/Admin/ProductCategories/Index.cshtml", listProductCategory);
        }
        public IActionResult CreateOrUpdate([FromQuery]int? productCategoryId)
        {
            var productCategory = new CreateOrUpdateProductCategoryViewModel();
            if (productCategoryId != null)
            {
                var pc = _productCategoryService.Get(productCategoryId.Value);
                productCategory.Id = pc.Id;
                productCategory.Name = pc.Name;
                productCategory.Description = pc.Description;
            }
            return View("~/Views/Admin/ProductCategories/CreateOrUpdate.cshtml", productCategory);
        }

        public IActionResult Save([FromForm]ProductCategoryDto input)
        {
            var userId = User.Claims.FirstOrDefault(c => c.Type == "Id").Value;
            bool result;
            if(input.Id == 0)
            {
                result = _productCategoryService.Add(input, Guid.Parse(userId));
            }
            else
            {
                result = _productCategoryService.Update(input);
            }
            if (result == true)
            {
                return RedirectToAction("Index", "AdminProductCategory");
            }
            return Json(new { StatusCode = 500, Message = "Error" });
        }

        public IActionResult Delete([FromQuery]int productCategoryId)
        {
            var result = _productCategoryService.Delete(productCategoryId);
            if (result == true)
            {
                return RedirectToAction("Index", "AdminProductCategory");
            }
            return Json(new { StatusCode = 500, Message = "Error" });
        }
    }
}
