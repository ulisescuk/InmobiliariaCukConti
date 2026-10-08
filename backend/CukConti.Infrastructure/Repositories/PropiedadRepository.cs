using System.Collections.Generic;
using System.Threading.Tasks;
using CukConti.Domain.Entities;
using CukConti.Domain.Interfaces;
using CukConti.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace CukConti.Infrastructure.Repositories
{
    public class PropiedadRepository : RepositorioBase<Propiedad>, IPropiedadRepository
    {
        public PropiedadRepository(CukContiDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Propiedad>> ListarPorPropietarioAsync(int propietarioId)
        {
            return await _dbSet.Where(p => p.PropietarioId == propietarioId).ToListAsync();
        }
    }
}
