using LadingPage.Common.Enums;
using LadingPage.Common.Enums.Extension;
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

        public bool Delete(int id)
        {
            var contact = _dbContext.CustomerContacts.Find(id);
            if (contact != null)
            {
                _dbContext.CustomerContacts.Remove(contact);
                var result = _dbContext.SaveChanges();
                return result > 0 ? true : false;
            }
            else
            {
                return false;
            }
           
        }

        public async Task<List<CustomerContactDto>> GetAll(string searchText)
        {
            return await _dbContext.CustomerContacts
                .Where(c =>
                    searchText == null || searchText == "" ||
                    c.FullName.Contains(searchText) || 
                    c.Address.Contains(searchText) ||
                    c.Question.Contains(searchText) ||
                    c.PhoneNumber.Contains(searchText) ||
                    c.Email.Contains(searchText) 
                )
                .OrderByDescending(c=>c.CreatedDate)
                .Select((c) => new CustomerContactDto()
                {
                    Email = c.Email,
                    FullName =c.FullName,
                    PhoneNumber = c.PhoneNumber,
                    CreatedDate = c.CreatedDate.Value.ToString("dd/MM/yyyy"),
                    Address = c.Address,
                    Question = c.Question,  
                    TicketAmount = c.TicketAmount,
                    TicketType = c.TicketType.DisplayName(),
                    Id = c.Id,
                }).ToListAsync();
        }

        public async Task<CustomerContactDto> GetById(int id)
        {
            return await _dbContext.CustomerContacts.Where(c => c.Id == id).Select(c => new CustomerContactDto()
            {
                Email = c.Email,
                FullName = c.FullName,
                PhoneNumber = c.PhoneNumber,
                CreatedDate = c.CreatedDate.Value.ToString("dd/MM/yyyy"),
                Address = c.Address,
                Question = c.Question,
                TicketAmount = c.TicketAmount,
                TicketType = c.TicketType.DisplayName(),
                Id = c.Id
            }).FirstOrDefaultAsync();
        }

        public async Task<CustomerContact> SaveCustomerContact(SaveContactModel request)
        {
            try
            {
                var contactModel = new CustomerContact()
                {
                    FullName = request.FullName,
                    PhoneNumber = request.PhoneNumber,
                    Email = request.Email,
                    TicketType=  (TicketType)request.TicketType,
                    TicketAmount= request.TicketAmount,
                    Address = request.Address,  
                    Question= request.Question,
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
            }
        }
    }
}
