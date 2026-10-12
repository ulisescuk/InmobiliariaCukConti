using CukConti.Application.DTOs;
using CukConti.Application.Interfaces;
using CukConti.Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CukConti.Api.Controllers
{
    [ApiController]
    [Route("api/pagos")]
    [Authorize]
    public class PagosController : ControllerBase
    {
        private readonly PagoService _service;
        private readonly IReciboService _reciboService;

        public PagosController(PagoService service, IReciboService reciboService)
        {
            _service = service;
            _reciboService = reciboService;
        }

        [HttpGet("contrato/{contratoId}")]
        public async Task<IActionResult> ListarPorContrato(int contratoId)
        {
            return Ok(await _service.ListarPorContratoAsync(contratoId));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> ObtenerPorId(int id)
        {
            var pago = await _service.ObtenerPorIdAsync(id);
            return pago is null ? NotFound() : Ok(pago);
        }

        [HttpPost]
        public async Task<IActionResult> Crear(CrearPagoRequest request)
        {
            try
            {
                var creado = await _service.CrearAsync(request);
                return CreatedAtAction(nameof(ObtenerPorId), new { id = creado.Id }, creado);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { mensaje = ex.Message });
            }
        }

        [HttpGet("{id}/recibo")]
        public async Task<IActionResult> DescargarRecibo(int id)
        {
            var pago = await _service.ObtenerPorIdAsync(id);
            if (pago is null) return NotFound();

            var pdf = _reciboService.GenerarReciboPago(
                pago.PropiedadDireccion ?? "-",
                pago.InquilinoNombre ?? "-",
                pago.Monto,
                pago.Periodo,
                pago.FechaPago,
                pago.MetodoPago);

            return File(pdf, "application/pdf", $"recibo-pago-{pago.Id}.pdf");
        }
    }
}
