using System;
using System.Collections.Generic;
using CukConti.Domain.Common;
using CukConti.Domain.Enums;

namespace CukConti.Domain.Entities
{
    public class Contrato : EntidadBase
    {
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
        public decimal MontoInicial { get; set; }
        public decimal MontoActual { get; set; }

        public TipoActualizacion TipoActualizacion { get; set; }
        public int? IndiceId { get; set; }
        public Indice? Indice { get; set; }
        public decimal? ValorPorcentaje { get; set; }

        public int FrecuenciaMeses { get; set; }
        public DateTime ProximaActualizacion { get; set; }
        public EstadoContrato Estado { get; set; } = EstadoContrato.Vigente;

        public int PropiedadId { get; set; }
        public Propiedad? Propiedad { get; set; }

        public int InquilinoId { get; set; }
        public Inquilino? Inquilino { get; set; }

        public int? ContratoAnteriorId { get; set; }
        public Contrato? ContratoAnterior { get; set; }

        public ICollection<Garante> Garantes { get; set; } = new List<Garante>();
        public ICollection<HistorialActualizacion> HistorialActualizaciones { get; set; } = new List<HistorialActualizacion>();
    }
}
