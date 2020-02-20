﻿using bakalaurinis.Infrastructure.Database.Models;
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

        public DatabaseContext(DbContextOptions options) : base(options) 
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

    }
}
