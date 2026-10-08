namespace CukConti.Application.DTOs
{
    public record CrearPropietarioRequest(string Nombre, string Apellido, string Dni, string? Telefono, string? Email);

    public record ActualizarPropietarioRequest(string Nombre, string Apellido, string Dni, string? Telefono, string? Email);

    public record PropietarioResponse(int Id, string Nombre, string Apellido, string Dni, string? Telefono, string? Email);
}
