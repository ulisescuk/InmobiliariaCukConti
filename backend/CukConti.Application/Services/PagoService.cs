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
    public class PagoService
    {
        private readonly IPagoRepository _pagoRepository;

        public PagoService(IPagoRepository pagoRepository)
        {
            _pagoRepository = pagoRepository;
        }

        private static PagoResponse AResponse(Pago p) => new(
            p.Id, p.FechaPago, p.Periodo, p.Monto, p.Estado.ToString(), p.MetodoPago,
            p.ContratoId, p.Contrato?.Propiedad?.Direccion,
            p.Contrato?.Inquilino is null ? null : $"{p.Contrato.Inquilino.Nombre} {p.Contrato.Inquilino.Apellido}");

        public async Task<IEnumerable<PagoResponse>> ListarPorContratoAsync(int contratoId)
        {
            var pagos = await _pagoRepository.ListarPorContratoAsync(contratoId);
            return pagos.Select(AResponse);
        }

        public async Task<PagoResponse?> ObtenerPorIdAsync(int id)
        {
            var pago = await _pagoRepository.ObtenerConDetalleAsync(id);
            return pago is null ? null : AResponse(pago);
        }

        public async Task<PagoResponse> CrearAsync(CrearPagoRequest request)
        {
            if (!Enum.TryParse<EstadoPago>(request.Estado, true, out var estado))
                throw new ArgumentException("Estado invalido. Debe ser Pagado, Pendiente o Vencido.");

            var pago = new Pago
            {
                ContratoId = request.ContratoId,
                FechaPago = request.FechaPago,
                Periodo = request.Periodo,
                Monto = request.Monto,
                Estado = estado,
                MetodoPago = request.MetodoPago
            };

            await _pagoRepository.AgregarAsync(pago);
            await _pagoRepository.GuardarCambiosAsync();

            var creado = await _pagoRepository.ObtenerConDetalleAsync(pago.Id);
            return AResponse(creado!);
        }
    }
}
