using System;
using System.Collections.Generic;
using System.Text;

namespace LadingPage.Common.ViewModel.Admin
{
     public class BlogsAdminViewModel : BaseViewModel
    {
        public string Title { get; set; }
        public string MetaTitle { get; set; }
        public string UrlImage { get; set; }
        public string Content { get; set; }
    }
}
