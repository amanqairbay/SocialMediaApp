using System;
using Core.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Infrastructure.Data
{
    public class ApplicationDbContext : IdentityDbContext<AppUser, IdentityRole<long>, long>
    {
        #region Fields
        public DbSet<Photo> Photos { get; set; } = default!;
        public DbSet<Region> Regions { get; set; } = default!;
        public DbSet<City> Cities { get; set; } = default!;
        public DbSet<Gender> Genders { get; set; } = default!;
        public DbSet<Status> Statuses { get; set; } = default!;
        #endregion Fields

        #region Constructor
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
        #endregion Constructor

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}

