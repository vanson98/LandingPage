using LandingPage.Domain.EF;
using LandingPage.Domain.Entities;
using LandingPage.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace LandingPage.Repository
{
    public class FeedBackRepository: BaseRepository<FeedBack>, IFeedBackRepository
    {
        public FeedBackRepository(ApplicationDbContext dbContext) : base(dbContext)
        {

        }
    }
}
