using System.Collections.Generic;
using System.Threading.Tasks;
using CukConti.Domain.Entities;

namespace CukConti.Domain.Interfaces
{
    public interface IContactoRepository : IRepositorioBase<Contacto>
    {
        Task<IEnumerable<Contacto>> ListarPorContratoAsync(int contratoId);
    }
}
