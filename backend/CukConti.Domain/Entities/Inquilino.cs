using System.Collections.Generic;
using CukConti.Domain.Common;

namespace CukConti.Domain.Entities
{
    public class Inquilino : EntidadBase
    {
        public string Nombre { get; set; } = string.Empty;
        public string Apellido { get; set; } = string.Empty;
        public string Dni { get; set; } = string.Empty;
        public string? Telefono { get; set; }
        public string? Email { get; set; }

        public ICollection<Contrato> Contratos { get; set; } = new List<Contrato>();
    }
}
