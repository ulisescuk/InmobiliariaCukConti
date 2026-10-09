using CukConti.Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CukConti.Api.Controllers
{
    [ApiController]
    [Route("api/alertas")]
    [Authorize]
    public class AlertasController : ControllerBase
    {
        private readonly AlertaService _alertaService;

        public AlertasController(AlertaService alertaService)
        {
            _alertaService = alertaService;
        }

        [HttpPost("generar")]
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> Generar()
        {
            var resultado = await _alertaService.GenerarAlertasDiariasAsync();
            return Ok(resultado);
        }
    }
}
