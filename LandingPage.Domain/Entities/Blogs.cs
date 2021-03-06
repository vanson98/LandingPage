using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LandingPage.Domain.Entities
{
    public class Blogs : BaseModel
    {
        public string Title { get; set; }
        public string MetaTitle { get; set; }
        public string UrlImage { get; set; }
        public string Content { get; set; }
        public Guid CategoryId { get; set; }

        public virtual Category Category { get; set; }



    }
}
