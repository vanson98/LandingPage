using LadingPage.Common.ViewModel.Admin;
using LandingPage.Domain;
using LandingPage.Domain.EF;
using LandingPage.Domain.Entities;
using LandingPage.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LandingPage.Repository
{
    public class BannerRepository : BaseRepository<Banner>, IBannerRepository
    {
        public BannerRepository(ApplicationDbContext dbContext) : base(dbContext)
        {

        }

      
    }
}
