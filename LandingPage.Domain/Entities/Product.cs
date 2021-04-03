using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace LandingPage.Domain.Entities
{
    public class Product : BaseModel
    {
        public string ProductCode { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Content { get; set; }
        public string Tag { get; set; }
        public bool Status { get; set; }
        public string MetaKeyWord { get; set; }
        public string MetaDescription { get; set; }
        public string MetaTitle { get; set; }
        public string ParentCode { get; set; }
        [ForeignKey("ProductCategory")]
        public int ProductCategoryId { get; set; }
        public virtual ProductCategory ProductCategory { get; set; }
        public virtual ICollection<ProductImage> ProductImages { get; set; }
    }
}
