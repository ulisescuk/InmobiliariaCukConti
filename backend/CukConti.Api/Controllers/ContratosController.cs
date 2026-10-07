using CukConti.Application.DTOs;
using CukConti.Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CukConti.Api.Controllers
{
    [ApiController]
    [Route("api/contratos")]
    [Authorize]
    public class ContratosController : ControllerBase
    {
        private readonly ContratoService _service;

        public ContratosController(ContratoService service)
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
            var contrato = await _service.ObtenerPorIdAsync(id);
            return contrato is null ? NotFound() : Ok(contrato);
        }

        [HttpPost]
        public async Task<IActionResult> Crear(CrearContratoRequest request)
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

        [HttpPost("{id}/renovar-nuevo")]
        public async Task<IActionResult> RenovarComoNuevo(int id, RenovarComoNuevoRequest request)
        {
            try
            {
                var renovado = await _service.RenovarComoNuevoAsync(id, request);
                return renovado is null ? NotFound() : Ok(renovado);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { mensaje = ex.Message });
            }
        }

        [HttpPut("{id}/extension")]
        public async Task<IActionResult> RenovarComoExtension(int id, RenovarComoExtensionRequest request)
        {
            var extendido = await _service.RenovarComoExtensionAsync(id, request);
            return extendido is null ? NotFound() : Ok(extendido);
        }
    }
}
