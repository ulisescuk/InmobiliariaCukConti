using System;
using System.ComponentModel.DataAnnotations;

namespace CukConti.Application.DTOs
{
    public record CrearPagoRequest(
        int ContratoId,
        DateTime FechaPago,
        [Required, StringLength(50)] string Periodo,
        [Range(0.01, double.MaxValue)] decimal Monto,
        [Required] string Estado,
        string? MetodoPago);

    public record PagoResponse(
        int Id, DateTime FechaPago, string Periodo, decimal Monto, string Estado, string? MetodoPago,
        int ContratoId, string? PropiedadDireccion, string? InquilinoNombre);
}
