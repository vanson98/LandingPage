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
        public string MetaTitle { get; set; }
        public string UrlImage { get; set; }
        public string Content { get; set; }

        [ForeignKey("BlogCaregory")]
        public Guid BlogCategoryId { get; set; }

        public  BlogCategory BlogCategory { get; set; }
    }
}
