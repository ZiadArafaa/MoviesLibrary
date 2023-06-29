using MoviesLibrary.Core.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoviesLibrary.EF.Data
{
    public class ApplicationDbContext :IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options):base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder ModelBuilder)
        {
            base.OnModelCreating(ModelBuilder);
            ModelBuilder.Entity<ApplicationUser>().ToTable("Users", schema: "Auth");
            ModelBuilder.Entity<IdentityRole>().ToTable("Roles", schema: "Auth");
            ModelBuilder.Entity<IdentityUserRole<string>>().ToTable("UserRoles", schema: "Auth");
            ModelBuilder.Entity<IdentityUserClaim<string>>().ToTable("UserClaims", schema: "Auth");
            ModelBuilder.Entity<IdentityUserToken<string>>().ToTable("UserTokens", schema: "Auth");
            ModelBuilder.Entity<IdentityUserLogin<string>>().ToTable("UserLogins", schema: "Auth");
            ModelBuilder.Entity<IdentityRoleClaim<string>>().ToTable("RoleClaims", schema: "Auth");

            ModelBuilder.Entity<ApplicationUser>(p =>
            {
                p.HasIndex(u => u.Email).IsUnique().HasFilter("[Email] IS NOT NULL");
                p.Property(u => u.UserName).IsRequired();
                p.Property(u => u.PasswordHash).IsRequired();
            });
            ModelBuilder.Entity<Movie>().ToTable("Movies", schema: "Library");
            ModelBuilder.Entity<Genere>().ToTable("Generes", schema: "Library");
            ModelBuilder.Entity<Movie>(p =>
            {
                p.Property(m => m.Title).HasMaxLength(100);
                p.Property(m => m.Description).HasMaxLength(500);
            });
            ModelBuilder.Entity<Genere>().Property(g => g.Id).ValueGeneratedOnAdd();
        }

        public DbSet<Movie> Movies { get; set; }
        public DbSet<Genere> Generes { get; set; }

    }
}