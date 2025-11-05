using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ProjetoMacacos.Models;

namespace ProjetoMacacos.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Veiculo> Veiculos { get; set; }

        public DbSet<Registros> Registros { get; set; }
        
        public DbSet<Clientes> Clientes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Veiculo>().ToTable("Veiculo");
            modelBuilder.Entity<Registros>().ToTable("Registros");
            modelBuilder.Entity<Clientes>().ToTable("Clientes");

            modelBuilder.Entity<Registros>()
            .HasOne(r => r.Veiculo)
            .WithMany()
            .HasForeignKey(r => r.VeiculoId)
            .OnDelete(DeleteBehavior.Cascade);

            base.OnModelCreating(modelBuilder);
        }
    }
}
