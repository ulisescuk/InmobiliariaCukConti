using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CukConti.Application.DTOs;
using CukConti.Domain.Interfaces;

namespace CukConti.Application.Services
{
    public class AuditoriaService
    {
        private readonly IRegistroCambioRepository _repository;

        public AuditoriaService(IRegistroCambioRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<RegistroCambioResponse>> ListarPorContratoAsync(int contratoId)
        {
            var registros = await _repository.ListarPorEntidadAsync("Contrato", contratoId);
            return registros.Select(r => new RegistroCambioResponse(r.Id, r.Campo, r.ValorAnterior, r.ValorNuevo, r.Fecha, r.Usuario?.Nombre));
        }

        public async Task<IEnumerable<RegistroCambioResponse>> ListarPorPagoAsync(int pagoId)
        {
            var registros = await _repository.ListarPorEntidadAsync("Pago", pagoId);
            return registros.Select(r => new RegistroCambioResponse(r.Id, r.Campo, r.ValorAnterior, r.ValorNuevo, r.Fecha, r.Usuario?.Nombre));
        }
    }
}
