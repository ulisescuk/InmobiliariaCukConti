using System.Collections.Generic;
using System.Threading.Tasks;
using CukConti.Domain.Entities;

namespace CukConti.Domain.Interfaces
{
    public interface IRegistroCambioRepository : IRepositorioBase<RegistroCambio>
    {
        Task<IEnumerable<RegistroCambio>> ListarPorEntidadAsync(string entidad, int idEntidad);
    }
}
