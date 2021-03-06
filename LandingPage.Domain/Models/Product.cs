using System;
using System.Collections.Generic;
using System.Text;

namespace LandingPage.Domain.Models
{
    public class Product : BaseModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public Guid CategoryId { get; set; }
        public virtual Category Category { get; set; }

        public virtual ICollection<ProductImage> ProductImages { get; set; }




    }
}
