using CukConti.Domain.Entities;
using CukConti.Domain.Interfaces;
using CukConti.Infrastructure.Persistence;

namespace CukConti.Infrastructure.Repositories
{
    public class MovimientoGeneralRepository : RepositorioBase<MovimientoGeneral>, IMovimientoGeneralRepository
    {
        public MovimientoGeneralRepository(CukContiDbContext context) : base(context)
        {
        }
    }
}
