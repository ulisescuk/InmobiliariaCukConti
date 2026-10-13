using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CukConti.Application.DTOs;
using CukConti.Domain.Entities;
using CukConti.Domain.Enums;
using CukConti.Domain.Interfaces;

namespace CukConti.Application.Services
{
    public class ContactoService
    {
        private readonly IContactoRepository _repository;

        public ContactoService(IContactoRepository repository)
        {
            _repository = repository;
        }

        private static ContactoResponse AResponse(Contacto c) =>
            new(c.Id, c.Fecha, c.Medio.ToString(), c.Detalle, c.ContratoId, c.Usuario?.Nombre);

        public async Task<IEnumerable<ContactoResponse>> ListarPorContratoAsync(int contratoId)
        {
            var contactos = await _repository.ListarPorContratoAsync(contratoId);
            return contactos.Select(AResponse);
        }

        public async Task<ContactoResponse> CrearAsync(CrearContactoRequest request, int usuarioId)
        {
            if (!Enum.TryParse<MedioContacto>(request.Medio, true, out var medio))
                throw new ArgumentException("Medio invalido. Debe ser Llamada, Mail o WhatsApp.");

            var contacto = new Contacto
            {
                ContratoId = request.ContratoId,
                Fecha = request.Fecha,
                Medio = medio,
                Detalle = request.Detalle,
                UsuarioId = usuarioId
            };

            await _repository.AgregarAsync(contacto);
            await _repository.GuardarCambiosAsync();

            return AResponse(contacto);
        }
    }
}
