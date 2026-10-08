using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CukConti.Application.DTOs;
using CukConti.Domain.Entities;
using CukConti.Domain.Interfaces;

namespace CukConti.Application.Services
{
    public class InquilinoService
    {
        private readonly IInquilinoRepository _repository;

        public InquilinoService(IInquilinoRepository repository)
        {
            _repository = repository;
        }

        private static InquilinoResponse AResponse(Inquilino i) =>
            new(i.Id, i.Nombre, i.Apellido, i.Dni, i.Telefono, i.Email);

        public async Task<IEnumerable<InquilinoResponse>> ListarAsync()
        {
            var inquilinos = await _repository.ListarAsync();
            return inquilinos.Select(AResponse);
        }

        public async Task<InquilinoResponse?> ObtenerPorIdAsync(int id)
        {
            var inquilino = await _repository.ObtenerPorIdAsync(id);
            return inquilino is null ? null : AResponse(inquilino);
        }

        public async Task<InquilinoResponse> CrearAsync(CrearInquilinoRequest request)
        {
            var inquilino = new Inquilino
            {
                Nombre = request.Nombre,
                Apellido = request.Apellido,
                Dni = request.Dni,
                Telefono = request.Telefono,
                Email = request.Email
            };

            await _repository.AgregarAsync(inquilino);
            await _repository.GuardarCambiosAsync();

            return AResponse(inquilino);
        }

        public async Task<InquilinoResponse?> ActualizarAsync(int id, ActualizarInquilinoRequest request)
        {
            var inquilino = await _repository.ObtenerPorIdAsync(id);
            if (inquilino is null) return null;

            inquilino.Nombre = request.Nombre;
            inquilino.Apellido = request.Apellido;
            inquilino.Dni = request.Dni;
            inquilino.Telefono = request.Telefono;
            inquilino.Email = request.Email;

            _repository.Actualizar(inquilino);
            await _repository.GuardarCambiosAsync();

            return AResponse(inquilino);
        }

        public async Task<bool> EliminarAsync(int id)
        {
            var inquilino = await _repository.ObtenerPorIdAsync(id);
            if (inquilino is null) return false;

            _repository.Eliminar(inquilino);
            await _repository.GuardarCambiosAsync();
            return true;
        }
    }
}
