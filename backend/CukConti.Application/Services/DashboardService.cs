using System.Threading.Tasks;
using CukConti.Application.DTOs;
using CukConti.Domain.Enums;
using CukConti.Domain.Interfaces;

namespace CukConti.Application.Services
{
    public class DashboardService
    {
        private readonly IContratoRepository _contratoRepository;
        private readonly IPagoRepository _pagoRepository;
        private readonly IMovimientoGeneralRepository _movimientoRepository;

        public DashboardService(
            IContratoRepository contratoRepository,
            IPagoRepository pagoRepository,
            IMovimientoGeneralRepository movimientoRepository)
        {
            _contratoRepository = contratoRepository;
            _pagoRepository = pagoRepository;
            _movimientoRepository = movimientoRepository;
        }

        public async Task<DashboardResponse> ObtenerResumenAsync()
        {
            var contratosProximos = await _contratoRepository.ContarProximosAVencerEsteMesAsync();
            var (cantidadPendientes, montoPendientes) = await _pagoRepository.ResumenPendientesAsync();
            var ingresosPagos = await _pagoRepository.SumarPagadosDelMesAsync();
            var ingresosMovimientos = await _movimientoRepository.SumarDelMesPorTipoAsync(TipoMovimiento.Ingreso);
            var egresosMovimientos = await _movimientoRepository.SumarDelMesPorTipoAsync(TipoMovimiento.Egreso);

            return new DashboardResponse(
                contratosProximos,
                cantidadPendientes,
                montoPendientes,
                ingresosPagos + ingresosMovimientos,
                egresosMovimientos
            );
        }
    }
}
