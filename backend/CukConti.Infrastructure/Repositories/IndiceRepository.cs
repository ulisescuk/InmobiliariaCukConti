using System.Threading.Tasks;
using CukConti.Domain.Entities;
using CukConti.Domain.Interfaces;
using CukConti.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace CukConti.Infrastructure.Repositories
{
    public class IndiceRepository : RepositorioBase<Indice>, IIndiceRepository
    {
        public IndiceRepository(CukContiDbContext context) : base(context)
        {
        }

        public async Task<Indice?> ObtenerPorNombreAsync(string nombre)
        {
            return await _dbSet.FirstOrDefaultAsync(i => i.Nombre == nombre);
        }
    }
}
