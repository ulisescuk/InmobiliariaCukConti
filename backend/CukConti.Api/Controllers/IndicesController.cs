using CukConti.Application.DTOs;
using CukConti.Application.Services;
using CukConti.Domain.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CukConti.Api.Controllers
{
    [ApiController]
    [Route("api/indices")]
    [Authorize]
    public class IndicesController : ControllerBase
    {
        private readonly SincronizacionIndicesService _sincronizacionService;
        private readonly IIndiceRepository _indiceRepository;
        private readonly IValorIndiceRepository _valorIndiceRepository;

        public IndicesController(
            SincronizacionIndicesService sincronizacionService,
            IIndiceRepository indiceRepository,
            IValorIndiceRepository valorIndiceRepository)
        {
            _sincronizacionService = sincronizacionService;
            _indiceRepository = indiceRepository;
            _valorIndiceRepository = valorIndiceRepository;
        }

        [HttpPost("sincronizar")]
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> Sincronizar(SincronizarIndiceRequest request)
        {
            try
            {
                var resultado = await _sincronizacionService.SincronizarAsync(request.Nombre);
                return Ok(new SincronizacionResponse(resultado.Indice, resultado.ValoresNuevos, resultado.UltimoPeriodo));
            }
            catch (ArgumentException ex)
            {
                return NotFound(new { mensaje = ex.Message });
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { mensaje = ex.Message });
            }
        }

        [HttpGet("{nombre}/valores")]
        public async Task<IActionResult> ListarValores(string nombre)
        {
            var indice = await _indiceRepository.ObtenerPorNombreAsync(nombre);
            if (indice is null) return NotFound();

            var valores = await _valorIndiceRepository.ListarPorIndiceAsync(indice.Id);
            return Ok(valores.Select(v => new ValorIndiceResponse(v.Periodo, v.Valor)));
        }
    }
}
