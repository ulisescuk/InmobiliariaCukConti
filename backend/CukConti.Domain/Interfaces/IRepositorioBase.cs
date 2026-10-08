using System.Collections.Generic;
using System.Threading.Tasks;
using CukConti.Domain.Common;

namespace CukConti.Domain.Interfaces
{
    public interface IRepositorioBase<T> where T : EntidadBase
    {
        Task<IEnumerable<T>> ListarAsync();
        Task<T?> ObtenerPorIdAsync(int id);
        Task AgregarAsync(T entidad);
        void Actualizar(T entidad);
        void Eliminar(T entidad);
        Task GuardarCambiosAsync();
    }
}
