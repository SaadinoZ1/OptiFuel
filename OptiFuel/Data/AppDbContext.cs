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
            modelBuilder.Entity<Planning>()
                 .HasOne(p => p.ValidationBL)
                 .WithOne(v => v.Planning)
                 .HasForeignKey<ValidationBL>(v => v.PlanningId);


            modelBuilder.Entity<Commission>()
                 .HasMany(c => c.Contact)
                 .WithMany() // You can add a join table here if needed
                 .UsingEntity<Dictionary<string, object>>(
                     "CommissionContact",
                     j => j.HasOne<Contact>().WithMany().HasForeignKey("ContactId"),
                     j => j.HasOne<Commission>().WithMany().HasForeignKey("CommissionId")
                 );


            modelBuilder.Entity<ValidationBL>()
                .HasOne(v => v.Commissions)
                .WithOne(c => c.ValidationBL)
                .HasForeignKey<Commission>(c => c.ValidationBLId);


            modelBuilder.Entity<Dechargement>()
                .HasOne(d => d.ValidationBL)
                .WithMany(v => v.Dechargements)
                .HasForeignKey(d => d.ValidationBLId);
        }


    }
}
