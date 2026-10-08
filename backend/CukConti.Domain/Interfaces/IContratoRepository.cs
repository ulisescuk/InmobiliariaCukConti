using System.Collections.Generic;
using System.Threading.Tasks;
using CukConti.Domain.Entities;

namespace CukConti.Domain.Interfaces
{
    public interface IContratoRepository : IRepositorioBase<Contrato>
    {
        Task<Contrato?> ObtenerConDetalleAsync(int id);
        Task<IEnumerable<Contrato>> ListarConDetalleAsync();
        Task<IEnumerable<Contrato>> ListarProximosAVencerAsync(int diasAntelacion);
    }
}
