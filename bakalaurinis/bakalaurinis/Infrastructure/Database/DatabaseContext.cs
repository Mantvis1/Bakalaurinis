using bakalaurinis.Infrastructure.Database.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Activity = bakalaurinis.Infrastructure.Database.Models.Activity;

namespace bakalaurinis.Infrastructure.Database
{
    public class DatabaseContext : IdentityDbContext<User>
    {
        private readonly IConfiguration _configuration;
        public DbSet<Activity> Activities { get; set; }

        public DatabaseContext(DbContextOptions options, IConfiguration configuration) : base(options) 
        {
            _configuration = configuration;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

    }
}
