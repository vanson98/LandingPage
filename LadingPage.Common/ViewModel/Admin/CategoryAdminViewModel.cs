using System;
using System.Collections.Generic;
using System.Text;

namespace LadingPage.Common.ViewModel.Admin
{
    public class CategoryAdminViewModel : BaseViewModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public Guid? ParentId { get; set; }
        public string URLImage { get; set; }
    }
}
