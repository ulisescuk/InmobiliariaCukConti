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
    public class PropiedadService
    {
        private readonly IPropiedadRepository _repository;

        public PropiedadService(IPropiedadRepository repository)
        {
            _repository = repository;
        }

        private static PropiedadResponse AResponse(Propiedad p) =>
            new(p.Id, p.Direccion, p.Tipo.ToString(), p.Estado.ToString(), p.PropietarioId);

        public async Task<IEnumerable<PropiedadResponse>> ListarAsync()
        {
            var propiedades = await _repository.ListarAsync();
            return propiedades.Select(AResponse);
        }

        public async Task<PropiedadResponse?> ObtenerPorIdAsync(int id)
        {
            var propiedad = await _repository.ObtenerPorIdAsync(id);
            return propiedad is null ? null : AResponse(propiedad);
        }

        public async Task<IEnumerable<PropiedadResponse>> ListarPorPropietarioAsync(int propietarioId)
        {
            var propiedades = await _repository.ListarPorPropietarioAsync(propietarioId);
            return propiedades.Select(AResponse);
        }

        public async Task<PropiedadResponse> CrearAsync(CrearPropiedadRequest request)
        {
            if (!Enum.TryParse<TipoPropiedad>(request.Tipo, true, out var tipo))
                throw new ArgumentException("Tipo invalido. Debe ser Alquiler o Venta.");

            var propiedad = new Propiedad
            {
                Direccion = request.Direccion,
                Tipo = tipo,
                Estado = EstadoPropiedad.Disponible,
                PropietarioId = request.PropietarioId
            };

            await _repository.AgregarAsync(propiedad);
            await _repository.GuardarCambiosAsync();

            return AResponse(propiedad);
        }

        public async Task<PropiedadResponse?> ActualizarAsync(int id, ActualizarPropiedadRequest request)
        {
            var propiedad = await _repository.ObtenerPorIdAsync(id);
            if (propiedad is null) return null;

            if (!Enum.TryParse<TipoPropiedad>(request.Tipo, true, out var tipo))
                throw new ArgumentException("Tipo invalido. Debe ser Alquiler o Venta.");

            if (!Enum.TryParse<EstadoPropiedad>(request.Estado, true, out var estado))
                throw new ArgumentException("Estado invalido. Debe ser Disponible, Ocupada o Vendida.");

            propiedad.Direccion = request.Direccion;
            propiedad.Tipo = tipo;
            propiedad.Estado = estado;

            _repository.Actualizar(propiedad);
            await _repository.GuardarCambiosAsync();

            return AResponse(propiedad);
        }

        public async Task<bool> EliminarAsync(int id)
        {
            var propiedad = await _repository.ObtenerPorIdAsync(id);
            if (propiedad is null) return false;

            _repository.Eliminar(propiedad);
            await _repository.GuardarCambiosAsync();
            return true;
        }
    }
}
