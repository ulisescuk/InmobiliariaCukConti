using System.Collections.Generic;
using System.Threading.Tasks;
using CukConti.Domain.Entities;

namespace CukConti.Domain.Interfaces
{
    public interface IPagoRepository : IRepositorioBase<Pago>
    {
        Task<IEnumerable<Pago>> ListarPorContratoAsync(int contratoId);
        Task<Pago?> ObtenerConDetalleAsync(int id);
    }
}
