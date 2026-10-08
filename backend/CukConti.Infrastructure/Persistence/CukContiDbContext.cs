using Microsoft.EntityFrameworkCore;
using CukConti.Domain.Entities;

namespace CukConti.Infrastructure.Persistence
{
    public class CukContiDbContext : DbContext
    {
        public CukContiDbContext(DbContextOptions<CukContiDbContext> options)
            : base(options) { }

        public DbSet<Usuario> Usuarios => Set<Usuario>();
        public DbSet<Propietario> Propietarios => Set<Propietario>();
        public DbSet<Inquilino> Inquilinos => Set<Inquilino>();
        public DbSet<Propiedad> Propiedades => Set<Propiedad>();
        public DbSet<Indice> Indices => Set<Indice>();
        public DbSet<ValorIndice> ValoresIndice => Set<ValorIndice>();
        public DbSet<Contrato> Contratos => Set<Contrato>();
        public DbSet<Garante> Garantes => Set<Garante>();
        public DbSet<HistorialActualizacion> HistorialActualizaciones => Set<HistorialActualizacion>();
        public DbSet<Pago> Pagos => Set<Pago>();
        public DbSet<Alerta> Alertas => Set<Alerta>();
        public DbSet<Contacto> Contactos => Set<Contacto>();
        public DbSet<RegistroCambio> RegistrosCambio => Set<RegistroCambio>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Usuario>()
                .HasIndex(u => u.Email)
                .IsUnique();

            modelBuilder.Entity<Contrato>()
                .HasOne(c => c.ContratoAnterior)
                .WithMany()
                .HasForeignKey(c => c.ContratoAnteriorId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Contrato>()
                .HasOne(c => c.Propiedad)
                .WithMany(p => p.Contratos)
                .HasForeignKey(c => c.PropiedadId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Contrato>()
                .HasOne(c => c.Inquilino)
                .WithMany(i => i.Contratos)
                .HasForeignKey(c => c.InquilinoId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Contrato>()
                .HasIndex(c => new { c.Estado, c.ProximaActualizacion });

            modelBuilder.Entity<ValorIndice>()
                .HasIndex(v => new { v.IndiceId, v.Periodo })
                .IsUnique();

            modelBuilder.Entity<Propietario>()
                .HasIndex(p => p.Dni)
                .IsUnique();

            modelBuilder.Entity<Inquilino>()
                .HasIndex(i => i.Dni)
                .IsUnique();
        }
    }
}