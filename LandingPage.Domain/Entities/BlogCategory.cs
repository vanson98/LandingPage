using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace LandingPage.Domain.Entities
{
    [Table("BlogCategories")]
    public class BlogCategory
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsDeleted { get; set; } = false;
        public ICollection<Blog> Blogs { get; set; }
    }
}
