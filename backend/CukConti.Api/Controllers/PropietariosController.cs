using CukConti.Application.DTOs;
using CukConti.Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CukConti.Api.Controllers
{
    [ApiController]
    [Route("api/propietarios")]
    [Authorize]
    public class PropietariosController : ControllerBase
    {
        private readonly PropietarioService _service;

        public PropietariosController(PropietarioService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> Listar()
        {
            return Ok(await _service.ListarAsync());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> ObtenerPorId(int id)
        {
            var propietario = await _service.ObtenerPorIdAsync(id);
            return propietario is null ? NotFound() : Ok(propietario);
        }

        [HttpPost]
        public async Task<IActionResult> Crear(CrearPropietarioRequest request)
        {
            var creado = await _service.CrearAsync(request);
            return CreatedAtAction(nameof(ObtenerPorId), new { id = creado.Id }, creado);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Actualizar(int id, ActualizarPropietarioRequest request)
        {
            var actualizado = await _service.ActualizarAsync(id, request);
            return actualizado is null ? NotFound() : Ok(actualizado);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Eliminar(int id)
        {
            var eliminado = await _service.EliminarAsync(id);
            return eliminado ? NoContent() : NotFound();
        }
    }
}
