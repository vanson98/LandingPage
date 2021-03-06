using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LandingPage.Domain.Entities
{
    public class Category : BaseModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public Guid? ParentId { get; set; }
        public string URLImage { get; set; }

        public virtual ICollection<Product> Products { get; set; }

        public virtual ICollection<Blogs> Blogs { get; set; }




    }
}
