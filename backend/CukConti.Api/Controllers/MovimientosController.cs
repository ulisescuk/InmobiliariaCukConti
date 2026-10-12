using System.Security.Claims;
using CukConti.Application.DTOs;
using CukConti.Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CukConti.Api.Controllers
{
    [ApiController]
    [Route("api/movimientos")]
    [Authorize]
    public class MovimientosController : ControllerBase
    {
        private readonly MovimientoGeneralService _service;

        public MovimientosController(MovimientoGeneralService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> Listar()
        {
            return Ok(await _service.ListarAsync());
        }

        [HttpPost]
        public async Task<IActionResult> Crear(CrearMovimientoRequest request)
        {
            var usuarioIdTexto = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var usuarioId = int.Parse(usuarioIdTexto!);

            try
            {
                var creado = await _service.CrearAsync(request, usuarioId);
                return Ok(creado);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { mensaje = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Eliminar(int id)
        {
            var eliminado = await _service.EliminarAsync(id);
            return eliminado ? NoContent() : NotFound();
        }
    }
}
