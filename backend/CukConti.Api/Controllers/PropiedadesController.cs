using CukConti.Application.DTOs;
using CukConti.Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CukConti.Api.Controllers
{
    [ApiController]
    [Route("api/propiedades")]
    [Authorize]
    public class PropiedadesController : ControllerBase
    {
        private readonly PropiedadService _service;

        public PropiedadesController(PropiedadService service)
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
            var propiedad = await _service.ObtenerPorIdAsync(id);
            return propiedad is null ? NotFound() : Ok(propiedad);
        }

        [HttpGet("por-propietario/{propietarioId}")]
        public async Task<IActionResult> ListarPorPropietario(int propietarioId)
        {
            return Ok(await _service.ListarPorPropietarioAsync(propietarioId));
        }

        [HttpPost]
        public async Task<IActionResult> Crear(CrearPropiedadRequest request)
        {
            try
            {
                var creada = await _service.CrearAsync(request);
                return CreatedAtAction(nameof(ObtenerPorId), new { id = creada.Id }, creada);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { mensaje = ex.Message });
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Actualizar(int id, ActualizarPropiedadRequest request)
        {
            try
            {
                var actualizada = await _service.ActualizarAsync(id, request);
                return actualizada is null ? NotFound() : Ok(actualizada);
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
