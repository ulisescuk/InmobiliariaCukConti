using CukConti.Domain.Entities;
using CukConti.Domain.Interfaces;
using CukConti.Infrastructure.Persistence;

namespace CukConti.Infrastructure.Repositories
{
    public class PropietarioRepository : RepositorioBase<Propietario>, IPropietarioRepository
    {
        public PropietarioRepository(CukContiDbContext context) : base(context)
        {
        }
    }
}
