using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using CrystalCards.Models;
using Microsoft.EntityFrameworkCore;

namespace CrystalCards.Data
{
    public class ApplicationDbContext:DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserProject>()
                .HasMany(c => c.Cards)
                .WithOne()
                .HasForeignKey(c => c.ProjectId)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<User>()
                .HasMany<CustomRole>(x => x.Roles)
                .WithOne()
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Card>()
                .HasMany<Link>(x => x.Links)
                .WithOne()
                .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<Card>()
                .HasMany<NPPoint>(x => x.Points)
                .WithOne()
                .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<Card>()
                .HasMany<ActionPoint>(x => x.ActionPoints)
                .WithOne()
                .OnDelete(DeleteBehavior.Cascade);

        }
    

        public virtual DbSet<Card> Cards { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<UserProject> Projects { get; set; }
    }
}
