using System;
using CukConti.Domain.Common;
using CukConti.Domain.Enums;

namespace CukConti.Domain.Entities
{
    public class Pago : EntidadBase
    {
        public DateTime FechaPago { get; set; }
        public string Periodo { get; set; } = string.Empty;
        public decimal Monto { get; set; }
        public EstadoPago Estado { get; set; } = EstadoPago.Pendiente;
        public string? MetodoPago { get; set; }

        public int ContratoId { get; set; }
        public Contrato? Contrato { get; set; }
    }
}
