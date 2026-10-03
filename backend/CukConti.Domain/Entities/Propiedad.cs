using System.Collections.Generic;
using CukConti.Domain.Common;
using CukConti.Domain.Enums;

namespace CukConti.Domain.Entities
{
    public class Propiedad : EntidadBase
    {
        public string Direccion { get; set; } = string.Empty;
        public TipoPropiedad Tipo { get; set; }
        public EstadoPropiedad Estado { get; set; } = EstadoPropiedad.Disponible;

        public int PropietarioId { get; set; }
        public Propietario? Propietario { get; set; }

        public ICollection<Contrato> Contratos { get; set; } = new List<Contrato>();
    }
}
