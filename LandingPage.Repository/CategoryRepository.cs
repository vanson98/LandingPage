using LandingPage.Domain;
using LandingPage.Domain.Models;
using LandingPage.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LandingPage.Repository
{
    public class CategoryRepository : BaseRepository<Category>, ICategoryRepository
    {
        public CategoryRepository(ApplicationDbContext dbContext) : base(dbContext)
        {

        }

        public Task<ICollection<Category>> GetCategoryParrent()
        {
            throw new NotImplementedException();
        }
    }
}
