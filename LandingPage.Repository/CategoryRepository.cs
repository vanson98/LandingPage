
using LandingPage.Domain.EF;
using LandingPage.Domain.Entities;
using LandingPage.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LandingPage.Repository
{
    public class CategoryRepository : BaseRepository<ProductCategory>, ICategoryRepository
    {
        public CategoryRepository(ApplicationDbContext dbContext) : base(dbContext)
        {

        }

        public Task<ICollection<ProductCategory>> GetCategoryParrent()
        {
            throw new NotImplementedException();
        }
    }
}
