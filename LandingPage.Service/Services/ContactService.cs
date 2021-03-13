using LandingPage.Domain.EF;
using LandingPage.Domain.Entities;
using LandingPage.Service.Dto;
using LandingPage.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LandingPage.Service.Services
{
    public class ContactService : IContactService
    {
        private readonly ApplicationDbContext _dbContext;

        public ContactService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<CustomerContactDto>> GetContactAdminViewModels()
        {
            return null;
        }

        public async Task<bool> SaveCustomerContact(CustomerContactDto request)
        {
            try
            {
                var contactModel = new ContactModel()
                {
                    Id = Guid.NewGuid(),
                    FirstName = request.FirstName,
                    LastName = request.LastName,
                    PhoneNumber = request.PhoneNumber,
                    Email = request.Email,
                    Message = request.Message
                };
                await _dbContext.ContactModels.AddAsync(contactModel);
                var result = await _dbContext.SaveChangesAsync();
                if (result == 0)
                {
                    return false;
                }
                return true;
            }
            catch (Exception e)
            {

                throw e;
            }
        
        }
    }
}
