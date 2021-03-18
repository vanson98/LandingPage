using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace LandingPage.Domain.Entities
{
    [Table("Blog")]
    public class Blog : BaseModel
    {
        public string Title { get; set; }
        public string ShortDescription { get; set; }
        public string UrlImage { get; set; }
        public string Content { get; set; }
        public bool Published { get; set; }
        public string MetaKeyWord { get; set; }
        public string MetaDescription { get; set; }
        public string MetaTitle { get; set; }
        [ForeignKey("BlogCaregory")]
        public int BlogCategoryId { get; set; }

        public  BlogCategory BlogCategory { get; set; }
    }
}
