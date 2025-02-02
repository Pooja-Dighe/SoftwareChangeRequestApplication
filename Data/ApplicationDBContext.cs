﻿using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SCRSApplication.Models;
using System.Reflection.Emit;
using System.Xml;

namespace SCRSApplication.Data
{
    public class ApplicationDBContext : IdentityDbContext
    {
        public ApplicationDBContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)     
        {
            base.OnModelCreating(builder);
            builder.HasDefaultSchema("Identity");          ///to change schema of database from dbo to identity
            builder.Entity<IdentityUser>(entity =>
            {
                entity.ToTable(name: "User");
            });
            builder.Entity<IdentityRole>(entity =>
            {
                entity.ToTable(name: "Role");
            });
            builder.Entity<IdentityUserRole<string>>(entity =>
            {
                entity.ToTable("UserRoles");
            });
            builder.Entity<IdentityUserClaim<string>>(entity =>
            {
                entity.ToTable("UserClaims");
            });
            builder.Entity<IdentityUserLogin<string>>(entity =>
            {
                entity.ToTable("UserLogins");
            });
            builder.Entity<IdentityRoleClaim<string>>(entity =>
            {
                entity.ToTable("RoleClaims");
            });
            builder.Entity<IdentityUserToken<string>>(entity =>
            {
                entity.ToTable("UserTokens");
            });
            builder.Entity<RaiseRequestEntity>()
                    .Property(e => e.RowVersion)
                    .HasColumnType("rowversion")
                    .ValueGeneratedOnAddOrUpdate()
                    .IsConcurrencyToken();

             builder.Entity<TeamMemberEntity>()
            .HasOne(e => e.User) // Navigation property
            .WithMany()
            .HasForeignKey(e => e.MemberId) // Set MemberId as FK
            .OnDelete(DeleteBehavior.Cascade); // Optional: Define delete behavior

            builder.Entity<TeamMemberEntity>()
          .HasOne(e => e.Team) // Navigation property
          .WithMany()
          .HasForeignKey(e => e.TeamId) // Set MemberId as FK
          .OnDelete(DeleteBehavior.Cascade); // Optional: Define delete behavior
        }


        public DbSet<ApplicationUser> ApplicationUser { get; set; }  //table name

        public DbSet<RaiseRequestEntity> RaiseRequestEntity { get; set; }

        public DbSet<ProjectEntity> ProjectEntity { get; set; }
        public DbSet<TeamsEntity> TeamEntity { get; set; }
        public DbSet<TeamMemberEntity> TeamMemberEntity {  get; set; }
    }
}
