using CukConti.Domain.Entities;
using CukConti.Domain.Interfaces;
using CukConti.Infrastructure.Persistence;

namespace CukConti.Infrastructure.Repositories
{
    public class InquilinoRepository : RepositorioBase<Inquilino>, IInquilinoRepository
    {
        public InquilinoRepository(CukContiDbContext context) : base(context)
        {
        }
    }
}
