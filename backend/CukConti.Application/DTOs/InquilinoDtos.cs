namespace CukConti.Application.DTOs
{
    public record CrearInquilinoRequest(string Nombre, string Apellido, string Dni, string? Telefono, string? Email);

    public record ActualizarInquilinoRequest(string Nombre, string Apellido, string Dni, string? Telefono, string? Email);

    public record InquilinoResponse(int Id, string Nombre, string Apellido, string Dni, string? Telefono, string? Email);
}
