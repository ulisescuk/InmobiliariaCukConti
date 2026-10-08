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
    public class ContratoRepository : RepositorioBase<Contrato>, IContratoRepository
    {
        public ContratoRepository(CukContiDbContext context) : base(context)
        {
        }

        private IQueryable<Contrato> ConDetalle()
        {
            return _dbSet
                .Include(c => c.Propiedad)
                .Include(c => c.Inquilino)
                .Include(c => c.Indice)
                .Include(c => c.Garantes)
                .Include(c => c.ContratoAnterior);
        }

        public async Task<Contrato?> ObtenerConDetalleAsync(int id)
        {
            return await ConDetalle().FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<IEnumerable<Contrato>> ListarConDetalleAsync()
        {
            return await ConDetalle().ToListAsync();
        }

        public async Task<IEnumerable<Contrato>> ListarProximosAVencerAsync(int diasAntelacion)
        {
            var fechaLimite = DateTime.UtcNow.AddDays(diasAntelacion);
            return await ConDetalle()
                .Where(c => c.Estado != EstadoContrato.Finalizado && c.ProximaActualizacion <= fechaLimite)
                .ToListAsync();
        }
    }
}
