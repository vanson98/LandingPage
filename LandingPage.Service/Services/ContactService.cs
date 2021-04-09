using LandingPage.Domain.EF;
using LandingPage.Domain.Entities;
using LandingPage.Service.Dto.Contact;
using LandingPage.Service.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public async Task<List<CustomerContactDto>> GetAll(string searchText)
        {
            return await _dbContext.CustomerContacts
                .Where(c =>
                    searchText == null || searchText == "" ||
                    c.FirstName.Contains(searchText) || 
                    c.LastName.Contains(searchText) ||
                    c.PhoneNumber.Contains(searchText) ||
                    c.Email.Contains(searchText) 
                )
                .OrderByDescending(c=>c.CreatedDate)
                .Select(c => new CustomerContactDto()
                {
                    Email = c.Email,
                    FullName = c.FirstName + " " + c.LastName,
                    PhoneNumber = c.PhoneNumber,
                    CreatedDate = c.CreatedDate.Value,
                    Id = c.Id
                }).ToListAsync();
        }

        public async Task<CustomerContactDto> GetById(int id)
        {
            return await _dbContext.CustomerContacts.Where(c => c.Id == id).Select(c => new CustomerContactDto()
            {
                Email = c.Email,
                FullName = c.FirstName + c.LastName,
                PhoneNumber = c.PhoneNumber,
                CreatedDate = c.CreatedDate.Value,
                Message = c.Message
            }).FirstOrDefaultAsync();
        }

        public async Task<CustomerContact> SaveCustomerContact(CustomerContactDto request)
        {
            try
            {
                var contactModel = new CustomerContact()
                {
                    FirstName = request.FirstName,
                    LastName = request.LastName,
                    PhoneNumber = request.PhoneNumber,
                    Email = request.Email,
                    Message = request.Message,
                    CreatedDate = DateTime.Now
                };
                var ct = await _dbContext.CustomerContacts.AddAsync(contactModel);
                var result = await _dbContext.SaveChangesAsync();
                if (result == 0)
                {
                    return null;
                }
                return contactModel;
            }
            catch (Exception e)
            {
                return null;
                throw e;
            }
        }
    }
}
