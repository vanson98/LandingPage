using System;
using System.Collections.Generic;
using System.Text;

namespace LandingPage.Domain.Entities
{
    public class ProductImage : BaseModel
    {
        public string UrlImage { get; set; }

        public bool IsMainImage { get; set; }
        public int ProductId { get; set; }
        public virtual Product Product { get; set; }


    }
}
