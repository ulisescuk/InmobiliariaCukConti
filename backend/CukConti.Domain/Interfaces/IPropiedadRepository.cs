using System.Collections.Generic;
using System.Threading.Tasks;
using CukConti.Domain.Entities;

namespace CukConti.Domain.Interfaces
{
    public interface IPropiedadRepository : IRepositorioBase<Propiedad>
    {
        Task<IEnumerable<Propiedad>> ListarPorPropietarioAsync(int propietarioId);
    }
}
