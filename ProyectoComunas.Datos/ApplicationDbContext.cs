using Microsoft.EntityFrameworkCore;
using ProyectoComunas.Datos.Models;

namespace ProyectoComunas.Datos
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Region> Regiones { get; set; }
        public DbSet<Comuna> Comunas { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Region>().ToTable("Region");
            modelBuilder.Entity<Comuna>().ToTable("Comuna");
        }
    }
}
