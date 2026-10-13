using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CukConti.Domain.Entities;
using CukConti.Domain.Interfaces;
using CukConti.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace CukConti.Infrastructure.Repositories
{
    public class ContactoRepository : RepositorioBase<Contacto>, IContactoRepository
    {
        public ContactoRepository(CukContiDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Contacto>> ListarPorContratoAsync(int contratoId)
        {
            return await _dbSet
                .Include(c => c.Usuario)
                .Where(c => c.ContratoId == contratoId)
                .OrderByDescending(c => c.Fecha)
                .ToListAsync();
        }
    }
}
