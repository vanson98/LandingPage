using System;
using System.Collections.Generic;
using System.Text;

namespace LadingPage.Common.ViewModel.Admin
{
    public class ContactAdminViewModel : BaseViewModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Message { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
    }
}
