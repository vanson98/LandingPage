using LandingPage.Domain.Entities;
using LandingPage.Service.Dto.Contact;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LandingPage.Service.Interfaces
{
    public interface IContactService
    {
        Task<List<CustomerContactDto>> GetAll(string searchText);
        Task<CustomerContactDto> GetById(int id);
        bool Delete(int id);
        Task<CustomerContact> SaveCustomerContact(SaveContactModel request);
    }
}
