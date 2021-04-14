using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LadingPage.Common.Utility;
using LandingPage.Models;
using LandingPage.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace LandingPage.Controllers
{
    public class EximaniProductController : Controller
    {
        private IProductService _productService;
        private IConfiguration Configuration;
        public EximaniProductController(IProductService productService, IConfiguration configuration)
        {
            _productService = productService;
            Configuration = configuration;
        }

        public IActionResult Index()
        {
            ViewBag.Title = Configuration["SeoConfig:Product:Title"];
            ViewBag.KeyWords = Configuration["SeoConfig:Product:KeyWords"];
            ViewBag.Descriptions = Configuration["SeoConfig:Product:Description"];
            var listExProdCategory = new List<ExhibitProductCategoryViewModel>();
            var listProductCategory = _productService.GetAllProductByCategoryOnView();
            foreach (var pc in listProductCategory)
            {
                var exProdCate = new ExhibitProductCategoryViewModel()
                {
                    CategoryId = pc.CategoryId,
                    CategoryName = pc.CategoryName,
                    ListExhibitProduct = pc.ListExhibitProduct.Select(p=>new ExhibitProductViewModel() { 
                        UrlMainImage = p.UrlMainImage,
                        LinkDetailProduct = Url.Action("Detail", "EximaniProduct", new {name = p.ProductName.GetSeoName()+"-"+p.ProductId}),
                        ProductName = p.ProductName
                    }).ToArray()
                };
                listExProdCategory.Add(exProdCate);
            }
            return View(listExProdCategory);
        }

        public async Task<IActionResult> Detail(string name)
        {
            var productId = Int32.Parse(name.Split("-").Last());
            var product = _productService.GetProductDetailById(productId);
            var detailProductVM = new DetailProductViewModel()
            {
                CategoryName = product.PorductCategoryName,
                Content = product.Content,
                Description = product.Description,
                Name = product.Name
            };
            detailProductVM.MainImageBase64 = _productService.GetMainImageOfProduct(product.Id.Value);
            detailProductVM.SubImagesBase64 = await _productService.GetListSubImageOfProduct(product.Id.Value,true);
            // Lấy sản phẩm liên quan
            //var listSubProduct = _productService.();
            //foreach (var subProd in listSubProduct)
            //{
            //    var subProdVM = new ExhibitProductCategoryViewModel()
            //    {
            //        Base64 = subProd.Base64,
            //        LinkDetailProduct = Url.Action("Detail", "EximaniProduct", new { id = subProd.ProductId, name = subProd.ProductName }),
            //        ProductName = subProd.ProductName
            //    };
            //    detailProductVM.ListSubProduct.Add(subProdVM);
            //}
            return View(detailProductVM);
        }
    }
}
