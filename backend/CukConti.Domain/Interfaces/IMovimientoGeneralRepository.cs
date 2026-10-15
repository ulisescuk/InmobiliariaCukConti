using System.Threading.Tasks;
using CukConti.Domain.Entities;
using CukConti.Domain.Enums;

namespace CukConti.Domain.Interfaces
{
    public interface IMovimientoGeneralRepository : IRepositorioBase<MovimientoGeneral>
    {
        Task<decimal> SumarDelMesPorTipoAsync(TipoMovimiento tipo);
    }
}
