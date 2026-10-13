using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CukConti.Domain.Entities;
using CukConti.Domain.Interfaces;
using CukConti.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace CukConti.Infrastructure.Repositories
{
    public class RegistroCambioRepository : RepositorioBase<RegistroCambio>, IRegistroCambioRepository
    {
        public RegistroCambioRepository(CukContiDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<RegistroCambio>> ListarPorEntidadAsync(string entidad, int idEntidad)
        {
            return await _dbSet
                .Include(r => r.Usuario)
                .Where(r => r.Entidad == entidad && r.IdEntidad == idEntidad)
                .OrderByDescending(r => r.Fecha)
                .ToListAsync();
        }
    }
}
