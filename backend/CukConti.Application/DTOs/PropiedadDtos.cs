namespace CukConti.Application.DTOs
{
    public record CrearPropiedadRequest(string Direccion, string Tipo, int PropietarioId);

    public record ActualizarPropiedadRequest(string Direccion, string Tipo, string Estado);

    public record PropiedadResponse(int Id, string Direccion, string Tipo, string Estado, int PropietarioId);
}
