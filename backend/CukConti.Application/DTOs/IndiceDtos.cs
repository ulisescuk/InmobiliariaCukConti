using System;

namespace CukConti.Application.DTOs
{
    public record SincronizarIndiceRequest(string Nombre);

    public record SincronizacionResponse(string Indice, int ValoresNuevos, DateTime? UltimoPeriodo);

    public record ValorIndiceResponse(DateTime Periodo, decimal Valor);
}
