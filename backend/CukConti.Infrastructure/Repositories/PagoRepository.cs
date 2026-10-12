using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CukConti.Domain.Entities;
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
    }
}
