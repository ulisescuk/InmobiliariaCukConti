using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using CukConti.Domain.Entities;

namespace CukConti.Infrastructure.Persistence
{
    public class CukContiDbContext : DbContext
    {
        private readonly IHttpContextAccessor? _httpContextAccessor;

        public CukContiDbContext(DbContextOptions<CukContiDbContext> options, IHttpContextAccessor? httpContextAccessor = null)
            : base(options)
        {
            _httpContextAccessor = httpContextAccessor;
        }

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
        public DbSet<MovimientoGeneral> MovimientosGenerales => Set<MovimientoGeneral>();

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

        private static readonly string[] CamposIgnorados = { "FechaCreacion" };

        private int? ObtenerUsuarioActualId()
        {
            var valor = _httpContextAccessor?.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            return int.TryParse(valor, out var id) ? id : null;
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var usuarioId = ObtenerUsuarioActualId();

            if (usuarioId is not null)
            {
                var registros = new List<RegistroCambio>();

                foreach (var entry in ChangeTracker.Entries())
                {
                    if (entry.State != EntityState.Modified) continue;
                    if (entry.Entity is not (Contrato or Pago)) continue;

                    var nombreEntidad = entry.Entity.GetType().Name;
                    var idEntidad = (int)entry.Property("Id").CurrentValue!;

                    foreach (var prop in entry.Properties)
                    {
                        if (!prop.IsModified) continue;
                        if (CamposIgnorados.Contains(prop.Metadata.Name)) continue;

                        var valorAnterior = prop.OriginalValue?.ToString();
                        var valorNuevo = prop.CurrentValue?.ToString();
                        if (valorAnterior == valorNuevo) continue;

                        registros.Add(new RegistroCambio
                        {
                            Entidad = nombreEntidad,
                            IdEntidad = idEntidad,
                            Campo = prop.Metadata.Name,
                            ValorAnterior = valorAnterior,
                            ValorNuevo = valorNuevo,
                            Fecha = DateTime.UtcNow,
                            UsuarioId = usuarioId.Value
                        });
                    }
                }

                if (registros.Count > 0)
                    await RegistrosCambio.AddRangeAsync(registros, cancellationToken);
            }

            return await base.SaveChangesAsync(cancellationToken);
        }
    }
}
