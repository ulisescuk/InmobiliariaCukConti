using System.Threading.Tasks;
using CukConti.Domain.Entities;

namespace CukConti.Domain.Interfaces
{
    public interface IAlertaRepository : IRepositorioBase<Alerta>
    {
        Task<bool> ExisteHoyAsync(int contratoId, int diasAntelacion);
    }
}
