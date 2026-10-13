using System;

namespace CukConti.Application.DTOs
{
    public record CrearContactoRequest(int ContratoId, DateTime Fecha, string Medio, string Detalle);

    public record ContactoResponse(int Id, DateTime Fecha, string Medio, string Detalle, int ContratoId, string? Usuario);
}
