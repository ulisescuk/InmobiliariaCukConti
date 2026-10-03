using System;
using CukConti.Domain.Common;
using CukConti.Domain.Enums;

namespace CukConti.Domain.Entities
{
    public class Contacto : EntidadBase
    {
        public DateTime Fecha { get; set; }
        public MedioContacto Medio { get; set; }
        public string Detalle { get; set; } = string.Empty;

        public int ContratoId { get; set; }
        public Contrato? Contrato { get; set; }

        public int UsuarioId { get; set; }
        public Usuario? Usuario { get; set; }
    }
}
