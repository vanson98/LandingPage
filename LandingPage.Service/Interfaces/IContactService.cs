using LandingPage.Service.Dto.Contact;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LandingPage.Service.Interfaces
{
    public interface IContactService
    {
        Task<bool> SaveCustomerContact(CustomerContactDto request);
        Task<List<CustomerContactDto>> GetContactAdminViewModels();
       

    }
}
