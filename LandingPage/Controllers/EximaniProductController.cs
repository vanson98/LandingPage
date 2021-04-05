using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LandingPage.Models;
using LandingPage.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace LandingPage.Controllers
{
    public class EximaniProductController : Controller
    {
        private IProductService _productService;

        public EximaniProductController(IProductService productService)
        {
            _productService = productService;
        }

        public IActionResult Index()
        {
            var listExProdCategory = new List<ExhibitProductCategoryViewModel>();
            var listProductCategory = _productService.GetAllProductCategoryOnView();
            foreach (var pc in listProductCategory)
            {
                var exProdCate = new ExhibitProductCategoryViewModel()
                {
                    CategoryId = pc.CategoryId,
                    CategoryName = pc.CategoryName,
                    ListExhibitProduct = pc.ListExhibitProduct.Select(p=>new ExhibitProductViewModel() { 
                        Base64 = p.Base64,
                        LinkDetailProduct = Url.Action("Detail", "EximaniProduct", new { id = p.ProductId, name = p.ProductName }),
                        ProductName = p.ProductName
                    }).ToArray()
                };
                listExProdCategory.Add(exProdCate);
            }
            return View(listExProdCategory);
        }

        public async Task<IActionResult> Detail(int id)
        {
            var product = _productService.GetProductDetailById(id);
            var detailProductVM = new DetailProductViewModel()
            {
                CategoryName = product.PorductCategoryName,
                Content = product.Content,
                Description = product.Description,
                Name = product.Name
            };
            detailProductVM.MainImageBase64 = await _productService.GetMainImageOfProduct(id);
            detailProductVM.SubImagesBase64 = await _productService.GetListSubImageOfProduct(id);
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
