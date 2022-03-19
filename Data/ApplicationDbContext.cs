using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using E_commerce_web.Models;
using Microsoft.AspNetCore.Identity;

namespace E_commerce_web.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }



        public DbSet<Admin> Admins { get; set; }

        public DbSet<Seller> Sellers { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);


            builder.Entity<IdentityRoleClaim<string>>()
                .ToTable("RoleClaims","security");
            builder.Entity<IdentityRole>()
                            .ToTable("Roles","security");
            builder.Entity<IdentityUserClaim<string>>()
                            .ToTable("UserClaims","security");
            builder.Entity<IdentityUserLogin<string>>()
                            .ToTable("UserLogins","security");
            builder.Entity<IdentityUserRole<string>>()
                            .ToTable("UserRoles","security");
            builder.Entity<ApplicationUser>()
                .ToTable("Users","security");
            builder.Entity<IdentityUserToken<string>>()
                            .ToTable("UserTokens","security");


            builder.Entity<ApplicationUser>()
                .Property(e => e.FirstName)
                .IsRequired();
            builder.Entity<ApplicationUser>()
                            .Property(e => e.LastName)
                            .IsRequired();
        }
    }
    }

