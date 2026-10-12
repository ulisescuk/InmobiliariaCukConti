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
    public class MovimientoGeneralService
    {
        private readonly IMovimientoGeneralRepository _repository;

        public MovimientoGeneralService(IMovimientoGeneralRepository repository)
        {
            _repository = repository;
        }

        private static MovimientoResponse AResponse(MovimientoGeneral m) => new(
            m.Id, m.Concepto, m.Monto, m.Fecha, m.Tipo.ToString(),
            m.Usuario is null ? null : m.Usuario.Nombre);

        public async Task<IEnumerable<MovimientoResponse>> ListarAsync()
        {
            var movimientos = await _repository.ListarAsync();
            return movimientos.Select(AResponse);
        }

        public async Task<MovimientoResponse> CrearAsync(CrearMovimientoRequest request, int usuarioId)
        {
            if (!Enum.TryParse<TipoMovimiento>(request.Tipo, true, out var tipo))
                throw new ArgumentException("Tipo invalido. Debe ser Ingreso o Egreso.");

            var movimiento = new MovimientoGeneral
            {
                Concepto = request.Concepto,
                Monto = request.Monto,
                Fecha = request.Fecha,
                Tipo = tipo,
                UsuarioId = usuarioId
            };

            await _repository.AgregarAsync(movimiento);
            await _repository.GuardarCambiosAsync();

            return AResponse(movimiento);
        }

        public async Task<bool> EliminarAsync(int id)
        {
            var movimiento = await _repository.ObtenerPorIdAsync(id);
            if (movimiento is null) return false;

            _repository.Eliminar(movimiento);
            await _repository.GuardarCambiosAsync();
            return true;
        }
    }
}
