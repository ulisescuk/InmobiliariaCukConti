using System.Threading.Tasks;
using CukConti.Domain.Entities;

namespace CukConti.Domain.Interfaces
{
    public interface IIndiceRepository : IRepositorioBase<Indice>
    {
        Task<Indice?> ObtenerPorNombreAsync(string nombre);
    }
}
