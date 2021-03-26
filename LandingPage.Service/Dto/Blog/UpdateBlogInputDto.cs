using System;
using System.Collections.Generic;
using System.Text;

namespace LandingPage.Service.Dto.Blog
{
    public class UpdateBlogInputDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string ShortDescription { get; set; }
        public string UrlImage { get; set; }
        public string Content { get; set; }
        public bool Published { get; set; }
        public string MetaKeyWord { get; set; }
        public string MetaDescription { get; set; }
        public string MetaTitle { get; set; }
        public int BlogCategoryId { get; set; }
    }
}
