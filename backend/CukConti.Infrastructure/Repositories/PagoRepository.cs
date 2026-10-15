using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CukConti.Domain.Entities;
using CukConti.Domain.Enums;
using CukConti.Domain.Interfaces;
using CukConti.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace CukConti.Infrastructure.Repositories
{
    public class PagoRepository : RepositorioBase<Pago>, IPagoRepository
    {
        public PagoRepository(CukContiDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Pago>> ListarPorContratoAsync(int contratoId)
        {
            return await _dbSet
                .Where(p => p.ContratoId == contratoId)
                .OrderByDescending(p => p.FechaPago)
                .ToListAsync();
        }

        public async Task<Pago?> ObtenerConDetalleAsync(int id)
        {
            return await _dbSet
                .Include(p => p.Contrato)
                    .ThenInclude(c => c!.Propiedad)
                .Include(p => p.Contrato)
                    .ThenInclude(c => c!.Inquilino)
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<(int Cantidad, decimal Total)> ResumenPendientesAsync()
        {
            var pendientes = _dbSet.Where(p => p.Estado == EstadoPago.Pendiente || p.Estado == EstadoPago.Vencido);
            var cantidad = await pendientes.CountAsync();
            var total = await pendientes.SumAsync(p => (decimal?)p.Monto) ?? 0;
            return (cantidad, total);
        }

        public async Task<decimal> SumarPagadosDelMesAsync()
        {
            var hoy = DateTime.UtcNow.Date;
            var inicioDeMes = new DateTime(hoy.Year, hoy.Month, 1);

            return await _dbSet
                .Where(p => p.Estado == EstadoPago.Pagado && p.FechaPago >= inicioDeMes && p.FechaPago <= hoy)
                .SumAsync(p => (decimal?)p.Monto) ?? 0;
        }
    }
}
