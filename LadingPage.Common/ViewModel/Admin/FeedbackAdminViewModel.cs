using System;
using System.Collections.Generic;
using System.Text;

namespace LadingPage.Common.ViewModel.Admin
{
    public class FeedbackAdminViewModel : BaseViewModel
    {
        public string AvatarUrl { get; set; }
        public string ClientName { get; set; }
        public string Country { get; set; }
        public string Content { get; set; }
    }
}
