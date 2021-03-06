using LadingPage.Common.ViewModel.Admin;
using LandingPage.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LandingPage.Repository.Interfaces
{
    public interface IBlogsRepository : IRepository<Blogs>
    {
        Task<List<BlogsAdminViewModel>> GetBlogsAdminViewModels();

    }
}
