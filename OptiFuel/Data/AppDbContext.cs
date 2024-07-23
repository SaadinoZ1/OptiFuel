using Microsoft.EntityFrameworkCore;
using OptiFuel.Models;

namespace OptiFuel.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        public DbSet<Planning> Planings { get; set; }
        public DbSet<BonDeLivraison> bonDeLivraisons { get; set; }

        public DbSet<Certificat> certificats { get; set; }
    }
}
