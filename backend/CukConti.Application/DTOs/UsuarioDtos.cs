namespace CukConti.Application.DTOs
{
    public record LoginRequest(string Email, string Password);

    public record LoginResponse(string Token, string Nombre, string Rol);

    public record CrearUsuarioRequest(string Nombre, string Email, string Password, string Rol);
}
