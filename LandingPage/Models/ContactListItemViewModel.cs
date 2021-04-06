using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LandingPage.Models
{
    public class ContactListItemViewModel
    {
        public int Id { get; set; }
        public int Index { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string CreatedDate { get; set; }
        public string SearchText { get; set; }
    }
}
