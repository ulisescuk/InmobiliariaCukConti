using System.Collections.Generic;
using CukConti.Domain.Common;

namespace CukConti.Domain.Entities
{
    public class Indice : EntidadBase
    {
        public string Nombre { get; set; } = string.Empty;
        public bool Activo { get; set; } = true;
        public int? IdVariableBcra { get; set; }

        public ICollection<Contrato> Contratos { get; set; } = new List<Contrato>();
        public ICollection<ValorIndice> Valores { get; set; } = new List<ValorIndice>();
    }
}
