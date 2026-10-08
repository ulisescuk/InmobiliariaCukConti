using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CukConti.Application.Interfaces;
using CukConti.Domain.Entities;
using CukConti.Domain.Interfaces;

namespace CukConti.Application.Services
{
    public record ResultadoSincronizacion(string Indice, int ValoresNuevos, DateTime? UltimoPeriodo);

    public class SincronizacionIndicesService
    {
        private readonly IIndiceRepository _indiceRepository;
        private readonly IValorIndiceRepository _valorIndiceRepository;
        private readonly IIndiceExternoService _indiceExterno;

        public SincronizacionIndicesService(
            IIndiceRepository indiceRepository,
            IValorIndiceRepository valorIndiceRepository,
            IIndiceExternoService indiceExterno)
        {
            _indiceRepository = indiceRepository;
            _valorIndiceRepository = valorIndiceRepository;
            _indiceExterno = indiceExterno;
        }

        public async Task<ResultadoSincronizacion> SincronizarAsync(string nombreIndice)
        {
            var indice = await _indiceRepository.ObtenerPorNombreAsync(nombreIndice);
            if (indice is null)
                throw new ArgumentException($"No existe un indice llamado '{nombreIndice}'.");

            if (indice.IdVariableBcra is null)
                throw new InvalidOperationException($"El indice '{nombreIndice}' no tiene configurada una variable del BCRA para sincronizar automaticamente.");

            var ultimoPeriodo = await _valorIndiceRepository.ObtenerUltimoPeriodoAsync(indice.Id);
            var desde = ultimoPeriodo?.AddDays(1) ?? DateTime.UtcNow.AddDays(-30);
            var hasta = DateTime.UtcNow;

            if (desde > hasta)
                return new ResultadoSincronizacion(indice.Nombre, 0, ultimoPeriodo);

            var valoresExternos = await _indiceExterno.ObtenerValoresAsync(indice.IdVariableBcra.Value, desde, hasta);

            int nuevos = 0;
            foreach (var v in valoresExternos)
            {
                var nuevoValor = new ValorIndice
                {
                    IndiceId = indice.Id,
                    Periodo = v.Fecha.Date,
                    Valor = v.Valor,
                    FechaObtencion = DateTime.UtcNow
                };
                await _valorIndiceRepository.AgregarAsync(nuevoValor);
                nuevos++;
            }

            if (nuevos > 0)
                await _valorIndiceRepository.GuardarCambiosAsync();

            var ultimoActualizado = valoresExternos.Any() ? valoresExternos.Max(v => v.Fecha) : ultimoPeriodo;

            return new ResultadoSincronizacion(indice.Nombre, nuevos, ultimoActualizado);
        }
    }
}
