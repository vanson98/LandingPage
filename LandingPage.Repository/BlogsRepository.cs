using LadingPage.Common.ViewModel.Admin;
using LandingPage.Domain;
using LandingPage.Domain.Models;
using LandingPage.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LandingPage.Repository
{
    public class BlogsRepository : BaseRepository<Blogs>, IBlogsRepository
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
