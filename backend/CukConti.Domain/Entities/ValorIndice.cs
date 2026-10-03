using System;
using CukConti.Domain.Common;

namespace CukConti.Domain.Entities
{
    public class ValorIndice : EntidadBase
    {
        public DateTime Periodo { get; set; }
        public decimal Valor { get; set; }
        public DateTime FechaObtencion { get; set; } = DateTime.UtcNow;

        public int IndiceId { get; set; }
        public Indice? Indice { get; set; }
    }
}
