using bakalaurinis.Infrastructure.Database.Models;
using Microsoft.EntityFrameworkCore;

namespace bakalaurinis.Infrastructure.Database
{
    public class DatabaseContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<UserSettings> UserSettings { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<Invitation> Invitations { get; set; }
        public DbSet<MessageTemplate> MessageTemplates { get; set; }
        public DbSet<Work> Works { get; set; }
        public DatabaseContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserSettings>().Property(x => x.StartTime).HasDefaultValue(8);
            modelBuilder.Entity<UserSettings>().Property(x => x.EndTime).HasDefaultValue(22);

            base.OnModelCreating(modelBuilder);

            InitialDataSeeder.CreateMessageTemplates(modelBuilder);
        }

    }
}
