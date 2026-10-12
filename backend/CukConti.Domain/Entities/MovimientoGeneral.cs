using System;
using CukConti.Domain.Common;
using CukConti.Domain.Enums;

namespace CukConti.Domain.Entities
{
    public class MovimientoGeneral : EntidadBase
    {
        public string Concepto { get; set; } = string.Empty;
        public decimal Monto { get; set; }
        public DateTime Fecha { get; set; }
        public TipoMovimiento Tipo { get; set; }

        public int UsuarioId { get; set; }
        public Usuario? Usuario { get; set; }
    }
}
