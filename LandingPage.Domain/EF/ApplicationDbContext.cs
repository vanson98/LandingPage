using LandingPage.Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace LandingPage.Domain.EF
{
    public class ApplicationDbContext : IdentityDbContext<AppUser,AppRole,Guid>
    {
        public ApplicationDbContext()
        {
        }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
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
