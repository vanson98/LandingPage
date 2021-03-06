using LandingPage.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LandingPage.Repository.Interfaces
{
    public interface ICategoryRepository : IRepository<Category>
    {
        Task<ICollection<Category>> GetCategoryParrent();


    }
}
