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
        Task<CustomerContact> SaveCustomerContact(CustomerContactDto request);
        Task<List<CustomerContactDto>> GetAll(string searchText);
        Task<CustomerContactDto> GetById(int id);

    }
}
