using System.ComponentModel.DataAnnotations;

namespace CukConti.Application.DTOs
{
    public record LoginRequest(
        [Required, EmailAddress] string Email,
        [Required] string Password);

    public record LoginResponse(string Token, string Nombre, string Rol);

    public record CrearUsuarioRequest(
        [Required, StringLength(100, MinimumLength = 2)] string Nombre,
        [Required, EmailAddress] string Email,
        [Required, StringLength(100, MinimumLength = 8)] string Password,
        [Required] string Rol);
}
