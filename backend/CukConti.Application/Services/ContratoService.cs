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
    public class ContratoService
    {
        private readonly IContratoRepository _contratoRepository;

        public ContratoService(IContratoRepository contratoRepository)
        {
            _contratoRepository = contratoRepository;
        }

        private static ContratoResponse AResponse(Contrato c) => new(
            c.Id,
            c.FechaInicio,
            c.FechaFin,
            c.MontoInicial,
            c.MontoActual,
            c.TipoActualizacion.ToString(),
            c.Indice?.Nombre,
            c.ValorPorcentaje,
            c.FrecuenciaMeses,
            c.ProximaActualizacion,
            c.Estado.ToString(),
            c.PropiedadId,
            c.Propiedad?.Direccion,
            c.InquilinoId,
            c.Inquilino is null ? null : $"{c.Inquilino.Nombre} {c.Inquilino.Apellido}",
            c.ContratoAnteriorId,
            c.Garantes.Select(g => new GaranteResponse(g.Id, g.Nombre, g.Apellido, g.Dni, g.Telefono)).ToList()
        );

        private static void ValidarReglaActualizacion(string tipoActualizacionTexto, int? indiceId, decimal? valorPorcentaje, out TipoActualizacion tipo)
        {
            if (!Enum.TryParse(tipoActualizacionTexto, true, out tipo))
                throw new ArgumentException("TipoActualizacion invalido. Debe ser Indice o Porcentaje.");

            if (tipo == TipoActualizacion.Indice && indiceId is null)
                throw new ArgumentException("Para actualizacion por Indice hay que indicar IndiceId.");

            if (tipo == TipoActualizacion.Porcentaje && valorPorcentaje is null)
                throw new ArgumentException("Para actualizacion por Porcentaje hay que indicar ValorPorcentaje.");
        }

        public async Task<IEnumerable<ContratoResponse>> ListarAsync()
        {
            var contratos = await _contratoRepository.ListarConDetalleAsync();
            return contratos.Select(AResponse);
        }

        public async Task<ContratoResponse?> ObtenerPorIdAsync(int id)
        {
            var contrato = await _contratoRepository.ObtenerConDetalleAsync(id);
            return contrato is null ? null : AResponse(contrato);
        }

        public async Task<ContratoResponse> CrearAsync(CrearContratoRequest request)
        {
            ValidarReglaActualizacion(request.TipoActualizacion, request.IndiceId, request.ValorPorcentaje, out var tipo);

            var contrato = new Contrato
            {
                FechaInicio = request.FechaInicio,
                FechaFin = request.FechaFin,
                MontoInicial = request.MontoInicial,
                MontoActual = request.MontoInicial,
                TipoActualizacion = tipo,
                IndiceId = tipo == TipoActualizacion.Indice ? request.IndiceId : null,
                ValorPorcentaje = tipo == TipoActualizacion.Porcentaje ? request.ValorPorcentaje : null,
                FrecuenciaMeses = request.FrecuenciaMeses,
                PropiedadId = request.PropiedadId,
                InquilinoId = request.InquilinoId,
                Estado = EstadoContrato.Vigente
            };

            contrato.CalcularProximaActualizacion();

            foreach (var g in request.Garantes)
            {
                contrato.Garantes.Add(new Garante
                {
                    Nombre = g.Nombre,
                    Apellido = g.Apellido,
                    Dni = g.Dni,
                    Telefono = g.Telefono
                });
            }

            await _contratoRepository.AgregarAsync(contrato);
            await _contratoRepository.GuardarCambiosAsync();

            var creado = await _contratoRepository.ObtenerConDetalleAsync(contrato.Id);
            return AResponse(creado!);
        }

        public async Task<ContratoResponse?> RenovarComoNuevoAsync(int contratoAnteriorId, RenovarComoNuevoRequest request)
        {
            var anterior = await _contratoRepository.ObtenerPorIdAsync(contratoAnteriorId);
            if (anterior is null) return null;

            ValidarReglaActualizacion(request.TipoActualizacion, request.IndiceId, request.ValorPorcentaje, out var tipo);

            var nuevo = new Contrato
            {
                FechaInicio = request.FechaInicio,
                FechaFin = request.FechaFin,
                MontoInicial = request.MontoInicial,
                MontoActual = request.MontoInicial,
                TipoActualizacion = tipo,
                IndiceId = tipo == TipoActualizacion.Indice ? request.IndiceId : null,
                ValorPorcentaje = tipo == TipoActualizacion.Porcentaje ? request.ValorPorcentaje : null,
                FrecuenciaMeses = request.FrecuenciaMeses,
                PropiedadId = anterior.PropiedadId,
                InquilinoId = anterior.InquilinoId,
                ContratoAnteriorId = anterior.Id,
                Estado = EstadoContrato.Vigente
            };

            nuevo.CalcularProximaActualizacion();

            foreach (var g in request.Garantes)
            {
                nuevo.Garantes.Add(new Garante
                {
                    Nombre = g.Nombre,
                    Apellido = g.Apellido,
                    Dni = g.Dni,
                    Telefono = g.Telefono
                });
            }

            anterior.Estado = EstadoContrato.Finalizado;
            _contratoRepository.Actualizar(anterior);

            await _contratoRepository.AgregarAsync(nuevo);
            await _contratoRepository.GuardarCambiosAsync();

            var creado = await _contratoRepository.ObtenerConDetalleAsync(nuevo.Id);
            return AResponse(creado!);
        }

        public async Task<ContratoResponse?> RenovarComoExtensionAsync(int contratoId, RenovarComoExtensionRequest request)
        {
            var contrato = await _contratoRepository.ObtenerConDetalleAsync(contratoId);
            if (contrato is null) return null;

            var montoAnterior = contrato.MontoActual;

            contrato.FechaFin = request.NuevaFechaFin;
            contrato.MontoActual = request.NuevoMonto;
            contrato.CalcularProximaActualizacion();

            contrato.HistorialActualizaciones.Add(new HistorialActualizacion
            {
                FechaActualizacion = DateTime.UtcNow,
                MontoAnterior = montoAnterior,
                MontoNuevo = request.NuevoMonto,
                Tipo = TipoMovimientoHistorial.Extension
            });

            _contratoRepository.Actualizar(contrato);
            await _contratoRepository.GuardarCambiosAsync();

            return AResponse(contrato);
        }
    }
}
