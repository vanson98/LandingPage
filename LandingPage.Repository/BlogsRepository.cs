using LadingPage.Common.ViewModel.Admin;
using LandingPage.Domain.EF;
using LandingPage.Domain.Entities;
using LandingPage.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LandingPage.Repository
{
    public class BlogsRepository : BaseRepository<Blog>, IBlogsRepository
    {
        public BlogsRepository(ApplicationDbContext dbContext) : base(dbContext)
        {

        }
        public Task<List<BlogsAdminViewModel>> GetBlogsAdminViewModels()
        {
            throw new NotImplementedException();
        }
    }
}
