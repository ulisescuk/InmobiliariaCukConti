using CukConti.Domain.Entities;

namespace CukConti.Application.Interfaces
{
    public interface IJwtTokenGenerator
    {
        string GenerarToken(Usuario usuario);
    }
}
