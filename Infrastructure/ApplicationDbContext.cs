using System;
using KocUniversityCourseManagement.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace KocUniversityCourseManagement.Infrastructure
{
    public class ApplicationDbContext : DbContext
    {
        private readonly IConfiguration _configuration;
        public ApplicationDbContext() { }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, IConfiguration configuration)
            : base(options)
        {
            _configuration = configuration;
        }
        public DbSet<ApplicationUser> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                string connstr = "Host=localhost:5432;Port=5432;Database=CourseDb;User Id=postgres_user;Password=postgres_password;";
                optionsBuilder.UseNpgsql(connstr);
            }
        }
    }
    

}

