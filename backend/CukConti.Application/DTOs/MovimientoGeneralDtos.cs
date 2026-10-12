using System;

namespace CukConti.Application.DTOs
{
    public record CrearMovimientoRequest(string Concepto, decimal Monto, DateTime Fecha, string Tipo);

    public record MovimientoResponse(int Id, string Concepto, decimal Monto, DateTime Fecha, string Tipo, string? UsuarioNombre);
}
