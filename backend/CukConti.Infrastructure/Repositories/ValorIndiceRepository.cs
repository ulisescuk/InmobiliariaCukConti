using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CukConti.Domain.Entities;
using CukConti.Domain.Interfaces;
using CukConti.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace CukConti.Infrastructure.Repositories
{
    public class ValorIndiceRepository : RepositorioBase<ValorIndice>, IValorIndiceRepository
    {
        public ValorIndiceRepository(CukContiDbContext context) : base(context)
        {
        }

        public async Task<DateTime?> ObtenerUltimoPeriodoAsync(int indiceId)
        {
            return await _dbSet
                .Where(v => v.IndiceId == indiceId)
                .OrderByDescending(v => v.Periodo)
                .Select(v => (DateTime?)v.Periodo)
                .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<ValorIndice>> ListarPorIndiceAsync(int indiceId)
        {
            return await _dbSet
                .Where(v => v.IndiceId == indiceId)
                .OrderByDescending(v => v.Periodo)
                .ToListAsync();
        }
    }
}
