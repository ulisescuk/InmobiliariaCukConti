using System;
using CukConti.Domain.Common;

namespace CukConti.Domain.Entities
{
    public class RegistroCambio : EntidadBase
    {
        public string Entidad { get; set; } = string.Empty;
        public int IdEntidad { get; set; }
        public string Campo { get; set; } = string.Empty;
        public string? ValorAnterior { get; set; }
        public string? ValorNuevo { get; set; }
        public DateTime Fecha { get; set; } = DateTime.UtcNow;

        public int UsuarioId { get; set; }
        public Usuario? Usuario { get; set; }
    }
}
