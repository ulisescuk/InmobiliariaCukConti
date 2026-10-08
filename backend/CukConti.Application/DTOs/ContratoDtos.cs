using System;
using System.Collections.Generic;

namespace CukConti.Application.DTOs
{
    public record GaranteRequest(string Nombre, string Apellido, string Dni, string? Telefono);

    public record GaranteResponse(int Id, string Nombre, string Apellido, string Dni, string? Telefono);

    public record CrearContratoRequest(
        DateTime FechaInicio,
        DateTime FechaFin,
        decimal MontoInicial,
        string TipoActualizacion,
        int? IndiceId,
        decimal? ValorPorcentaje,
        int FrecuenciaMeses,
        int PropiedadId,
        int InquilinoId,
        List<GaranteRequest> Garantes
    );

    public record RenovarComoNuevoRequest(
        DateTime FechaInicio,
        DateTime FechaFin,
        decimal MontoInicial,
        string TipoActualizacion,
        int? IndiceId,
        decimal? ValorPorcentaje,
        int FrecuenciaMeses,
        List<GaranteRequest> Garantes
    );

    public record RenovarComoExtensionRequest(DateTime NuevaFechaFin, decimal NuevoMonto);

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
