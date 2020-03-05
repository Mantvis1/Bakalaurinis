using bakalaurinis.Infrastructure.Database.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Activity = bakalaurinis.Infrastructure.Database.Models.Activity;

namespace bakalaurinis.Infrastructure.Database
{
    public class DatabaseContext : DbContext
    {
        public DbSet<Activity> Activities { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserSettings> UserSettings { get; set; }
        public DbSet<Invitations> Invitations { get; set; }

        public DatabaseContext(DbContextOptions options) : base(options) 
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserSettings>().Property(x => x.StartTime).HasDefaultValue(8);
            modelBuilder.Entity<UserSettings>().Property(x => x.EndTime).HasDefaultValue(22);

            modelBuilder.Entity<Invitations>()
                .HasKey(x => new { x.ActivityId, x.InviterId, x.RecieverId });

            modelBuilder.Entity<Invitations>()
                .HasOne(x => x.Inviter)
                .WithMany(x => x.SentInvitations)
                .HasForeignKey(x => x.InviterId);

            modelBuilder.Entity<Invitations>()
                .HasOne(x => x.Reciever)
                .WithMany(x => x.RecieveInvitations)
                .HasForeignKey(x => x.RecieverId);

            modelBuilder.Entity<Invitations>()
                .HasOne(x => x.Activity)
                .WithMany(x => x.Invitations)
                .HasForeignKey(x => x.ActivityId);
                

            base.OnModelCreating(modelBuilder);
        }

    }
}
