using LandingPage.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace LandingPage.Domain
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Banner> Banner { get; set; }
        public DbSet<Blogs> Blogs { get; set; }
        public DbSet<Category> Category { get; set; }
        public DbSet<ContactModel> ContactModels { get; set; }
        public DbSet<FeedBack> FeedBacks { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductImage> ProductImages { get; set; }







    }
}
