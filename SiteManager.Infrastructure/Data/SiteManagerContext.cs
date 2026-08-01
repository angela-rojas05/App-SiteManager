using Microsoft.EntityFrameworkCore;
using SiteManager.Domain.Models;

namespace SiteManager.Infrastructure.Data
{
    public class SiteManagerContext : DbContext
    {
        public SiteManagerContext(DbContextOptions<SiteManagerContext> options) : base(options) { }

        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Siniestro> Siniestros { get; set; }
        public DbSet<Evidencia> Evidencias { get; set; }
        public DbSet<Cotizacion> Cotizaciones { get; set; }
        public DbSet<Material> Materiales { get; set; }
        public DbSet<Reporte> Reportes { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Siniestro → Cliente
            modelBuilder.Entity<Siniestro>()
                .HasOne(s => s.Cliente)
                .WithMany(c => c.Siniestros)
                .HasForeignKey(s => s.ClienteId)
                .OnDelete(DeleteBehavior.Restrict);

            // Evidencia → Siniestro
            modelBuilder.Entity<Evidencia>()
                .HasOne(e => e.Siniestro)
                .WithMany(s => s.Evidencias)
                .HasForeignKey(e => e.SiniestroId)
                .OnDelete(DeleteBehavior.Cascade);

            // Cotizacion → Siniestro
            modelBuilder.Entity<Cotizacion>()
                .HasOne(c => c.Siniestro)
                .WithMany(s => s.Cotizaciones)
                .HasForeignKey(c => c.SiniestroId)
                .OnDelete(DeleteBehavior.Cascade);

            // Reporte → Siniestro
            modelBuilder.Entity<Reporte>()
                .HasOne(r => r.Siniestro)
                .WithMany()
                .HasForeignKey(r => r.SiniestroId)
                .OnDelete(DeleteBehavior.Restrict);

            // Material → Siniestro (opcional)
            modelBuilder.Entity<Material>()
                .HasOne(m => m.Siniestro)
                .WithMany()
                .HasForeignKey(m => m.SiniestroId)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.SetNull);

            // EstadoSiniestro como string
            modelBuilder.Entity<Siniestro>()
                .Property(s => s.Estado)
                .HasConversion<string>();

            // RolUsuario como string
            modelBuilder.Entity<Usuario>()
                .Property(u => u.Rol)
                .HasConversion<string>();
        }
    }
}