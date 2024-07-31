using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using OptiFuel.Models;

namespace OptiFuel.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        public DbSet<Planning> Plannings { get; set; }
        public DbSet<ValidationBL> validationBLs { get; set; }

        public DbSet<Commission> Commissions{  get; set; }
        public DbSet<Centre> centres { get; set; }
        public DbSet<Contact> contacts {get; set; }
        public DbSet<Dechargement> Dechargements { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            // Configuration des relations et des clés étrangères
            modelBuilder.Entity<Commission>()
                .HasOne(c => c.ValidationBL)
                .WithMany(v => v.Commissions)
                .HasForeignKey(c => c.ValidationBLId);

            modelBuilder.Entity<ValidationBL>()
                .HasOne(v => v.Planning)
                .WithOne(p => p.ValidationBL)
                .HasForeignKey<ValidationBL>(v => v.PlanningId);

            modelBuilder.Entity<Dechargement>()
                .HasOne(d => d.ValidationBL)
                .WithMany(v => v.Dechargements)
                .HasForeignKey(d => d.ValidationBLId);
        }


    }
}
