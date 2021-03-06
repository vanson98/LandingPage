
using LandingPage.Domain.EF;
using LandingPage.Domain.Entities;
using LandingPage.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace LandingPage.Repository
{
    public class ContactRepository : BaseRepository<ContactModel>, IContactRepository
    {
        public ContactRepository(ApplicationDbContext dbContext) : base(dbContext)
        {

        }
    }
}
