using System.ComponentModel.DataAnnotations;

namespace CukConti.Application.DTOs
{
    public record CrearPropietarioRequest(
        [Required, StringLength(80, MinimumLength = 2)] string Nombre,
        [Required, StringLength(80, MinimumLength = 2)] string Apellido,
        [Required, StringLength(10, MinimumLength = 7)] string Dni,
        [Phone] string? Telefono,
        [EmailAddress] string? Email);

    public record ActualizarPropietarioRequest(
        [Required, StringLength(80, MinimumLength = 2)] string Nombre,
        [Required, StringLength(80, MinimumLength = 2)] string Apellido,
        [Required, StringLength(10, MinimumLength = 7)] string Dni,
        [Phone] string? Telefono,
        [EmailAddress] string? Email);

    public record PropietarioResponse(int Id, string Nombre, string Apellido, string Dni, string? Telefono, string? Email);
}
