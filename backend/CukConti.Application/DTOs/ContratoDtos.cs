using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CukConti.Application.DTOs
{
    public record GaranteRequest(
        [Required, StringLength(80, MinimumLength = 2)] string Nombre,
        [Required, StringLength(80, MinimumLength = 2)] string Apellido,
        [Required, StringLength(10, MinimumLength = 7)] string Dni,
        [Phone] string? Telefono);

    public record GaranteResponse(int Id, string Nombre, string Apellido, string Dni, string? Telefono);

    public record CrearContratoRequest(
        DateTime FechaInicio,
        DateTime FechaFin,
        [Range(0.01, double.MaxValue)] decimal MontoInicial,
        [Required] string TipoActualizacion,
        int? IndiceId,
        [Range(0.01, 100)] decimal? ValorPorcentaje,
        [Range(1, 24)] int FrecuenciaMeses,
        int PropiedadId,
        int InquilinoId,
        List<GaranteRequest> Garantes
    );

    public record RenovarComoNuevoRequest(
        DateTime FechaInicio,
        DateTime FechaFin,
        [Range(0.01, double.MaxValue)] decimal MontoInicial,
        [Required] string TipoActualizacion,
        int? IndiceId,
        [Range(0.01, 100)] decimal? ValorPorcentaje,
        [Range(1, 24)] int FrecuenciaMeses,
        List<GaranteRequest> Garantes
    );

    public record RenovarComoExtensionRequest(
        DateTime NuevaFechaFin,
        [Range(0.01, double.MaxValue)] decimal NuevoMonto);

    public record ContratoResponse(
        int Id,
        DateTime FechaInicio,
        DateTime FechaFin,
        decimal MontoInicial,
        decimal MontoActual,
        string TipoActualizacion,
        string? Indice,
        decimal? ValorPorcentaje,
        int FrecuenciaMeses,
        DateTime ProximaActualizacion,
        string Estado,
        int PropiedadId,
        string? PropiedadDireccion,
        int InquilinoId,
        string? InquilinoNombre,
        int? ContratoAnteriorId,
        List<GaranteResponse> Garantes
    );
}
