using System;
using CukConti.Domain.Common;
using CukConti.Domain.Enums;

namespace CukConti.Domain.Entities
{
    public class HistorialActualizacion : EntidadBase
    {
        public DateTime FechaActualizacion { get; set; }
        public decimal MontoAnterior { get; set; }
        public decimal MontoNuevo { get; set; }
        public TipoMovimientoHistorial Tipo { get; set; }

        public int ContratoId { get; set; }
        public Contrato? Contrato { get; set; }
    }
}
