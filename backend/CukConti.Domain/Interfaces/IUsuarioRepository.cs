using System.Collections.Generic;
using System.Threading.Tasks;
using CukConti.Domain.Entities;

namespace CukConti.Domain.Interfaces
{
    public interface IUsuarioRepository
    {
        Task<Usuario?> ObtenerPorEmailAsync(string email);
        Task<Usuario?> ObtenerPorIdAsync(int id);
        Task<IEnumerable<Usuario>> ListarSecretariosActivosAsync();
        Task AgregarAsync(Usuario usuario);
        Task GuardarCambiosAsync();
    }
}
