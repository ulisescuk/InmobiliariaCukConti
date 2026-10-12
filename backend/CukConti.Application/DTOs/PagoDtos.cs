using System;

namespace CukConti.Application.DTOs
{
    public record CrearPagoRequest(int ContratoId, DateTime FechaPago, string Periodo, decimal Monto, string Estado, string? MetodoPago);

    public record PagoResponse(
        int Id, DateTime FechaPago, string Periodo, decimal Monto, string Estado, string? MetodoPago,
        int ContratoId, string? PropiedadDireccion, string? InquilinoNombre);
}
