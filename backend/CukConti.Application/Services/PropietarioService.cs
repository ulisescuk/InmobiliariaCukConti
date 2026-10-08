using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CukConti.Application.DTOs;
using CukConti.Domain.Entities;
using CukConti.Domain.Interfaces;

namespace CukConti.Application.Services
{
    public class PropietarioService
    {
        private readonly IPropietarioRepository _repository;

        public PropietarioService(IPropietarioRepository repository)
        {
            _repository = repository;
        }

        private static PropietarioResponse AResponse(Propietario p) =>
            new(p.Id, p.Nombre, p.Apellido, p.Dni, p.Telefono, p.Email);

        public async Task<IEnumerable<PropietarioResponse>> ListarAsync()
        {
            var propietarios = await _repository.ListarAsync();
            return propietarios.Select(AResponse);
        }

        public async Task<PropietarioResponse?> ObtenerPorIdAsync(int id)
        {
            var propietario = await _repository.ObtenerPorIdAsync(id);
            return propietario is null ? null : AResponse(propietario);
        }

        public async Task<PropietarioResponse> CrearAsync(CrearPropietarioRequest request)
        {
            var propietario = new Propietario
            {
                Nombre = request.Nombre,
                Apellido = request.Apellido,
                Dni = request.Dni,
                Telefono = request.Telefono,
                Email = request.Email
            };

            await _repository.AgregarAsync(propietario);
            await _repository.GuardarCambiosAsync();

            return AResponse(propietario);
        }

        public async Task<PropietarioResponse?> ActualizarAsync(int id, ActualizarPropietarioRequest request)
        {
            var propietario = await _repository.ObtenerPorIdAsync(id);
            if (propietario is null) return null;

            propietario.Nombre = request.Nombre;
            propietario.Apellido = request.Apellido;
            propietario.Dni = request.Dni;
            propietario.Telefono = request.Telefono;
            propietario.Email = request.Email;

            _repository.Actualizar(propietario);
            await _repository.GuardarCambiosAsync();

            return AResponse(propietario);
        }

        public async Task<bool> EliminarAsync(int id)
        {
            var propietario = await _repository.ObtenerPorIdAsync(id);
            if (propietario is null) return false;

            _repository.Eliminar(propietario);
            await _repository.GuardarCambiosAsync();
            return true;
        }
    }
}
