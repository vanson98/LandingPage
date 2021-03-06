using System;
using System.Collections.Generic;
using System.Text;

namespace LadingPage.Common.ViewModel.Admin
{
    public class ProductImageAdminViewModel
    {
        public string UrlImage { get; set; }
        public bool IsMainImage { get; set; }
        public Guid ProductId { get; set; }
    }
}
