using CukConti.Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CukConti.Api.Controllers
{
    [ApiController]
    [Route("api/auditoria")]
    [Authorize]
    public class AuditoriaController : ControllerBase
    {
        private readonly AuditoriaService _service;

        public AuditoriaController(AuditoriaService service)
        {
            _service = service;
        }

        [HttpGet("contrato/{contratoId}")]
        public async Task<IActionResult> ListarPorContrato(int contratoId)
        {
            return Ok(await _service.ListarPorContratoAsync(contratoId));
        }

        [HttpGet("pago/{pagoId}")]
        public async Task<IActionResult> ListarPorPago(int pagoId)
        {
            return Ok(await _service.ListarPorPagoAsync(pagoId));
        }
    }
}
