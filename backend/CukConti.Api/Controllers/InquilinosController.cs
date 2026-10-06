using CukConti.Application.DTOs;
using CukConti.Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CukConti.Api.Controllers
{
    [ApiController]
    [Route("api/inquilinos")]
    [Authorize]
    public class InquilinosController : ControllerBase
    {
        private readonly InquilinoService _service;

        public InquilinosController(InquilinoService service)
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
            var inquilino = await _service.ObtenerPorIdAsync(id);
            return inquilino is null ? NotFound() : Ok(inquilino);
        }

        [HttpPost]
        public async Task<IActionResult> Crear(CrearInquilinoRequest request)
        {
            var creado = await _service.CrearAsync(request);
            return CreatedAtAction(nameof(ObtenerPorId), new { id = creado.Id }, creado);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Actualizar(int id, ActualizarInquilinoRequest request)
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
