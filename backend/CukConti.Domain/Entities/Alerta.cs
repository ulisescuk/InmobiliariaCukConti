using System;
using CukConti.Domain.Common;
using CukConti.Domain.Enums;

namespace CukConti.Domain.Entities
{
    public class Alerta : EntidadBase
    {
        public TipoAlerta Tipo { get; set; }
        public string Mensaje { get; set; } = string.Empty;
        public int? DiasAntelacion { get; set; }
        public DateTime FechaGeneracion { get; set; } = DateTime.UtcNow;
        public bool EnviadaPorMail { get; set; }
        public bool Leida { get; set; }

        public int UsuarioId { get; set; }
        public Usuario? Usuario { get; set; }

        public int? ContratoId { get; set; }
        public Contrato? Contrato { get; set; }
    }
}
