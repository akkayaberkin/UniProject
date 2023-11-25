using System;
using KocUniversityCourseManagement.Domain;
using Microsoft.EntityFrameworkCore;

namespace KocUniversityCourseManagement.Infrastructure
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext() { }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<ApplicationUser> Users { get; set; }



        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
           if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseNpgsql("PostgresConnection");
            }
          
        }
    }
    

}

