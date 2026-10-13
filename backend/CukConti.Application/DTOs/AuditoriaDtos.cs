using System;

namespace CukConti.Application.DTOs
{
    public record RegistroCambioResponse(int Id, string Campo, string? ValorAnterior, string? ValorNuevo, DateTime Fecha, string? Usuario);
}
