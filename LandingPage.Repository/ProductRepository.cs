﻿using LandingPage.Domain.EF;
using LandingPage.Domain.Entities;
using LandingPage.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace LandingPage.Repository
{
    public class ProductRepository : BaseRepository<Product>, IProductRepository
    {
        public ProductRepository(ApplicationDbContext dbContext) : base(dbContext)
        {

        }
    }
}
