using CukConti.Domain.Common;

namespace CukConti.Domain.Entities
{
    public class Garante : EntidadBase
    {
        public string Nombre { get; set; } = string.Empty;
        public string Apellido { get; set; } = string.Empty;
        public string Dni { get; set; } = string.Empty;
        public string? Telefono { get; set; }

        public int ContratoId { get; set; }
        public Contrato? Contrato { get; set; }
    }
}
