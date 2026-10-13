using System.Security.Claims;
using CukConti.Application.DTOs;
using CukConti.Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CukConti.Api.Controllers
{
    [ApiController]
    [Route("api/contactos")]
    [Authorize]
    public class ContactosController : ControllerBase
    {
        private readonly ContactoService _service;

        public ContactosController(ContactoService service)
        {
            _service = service;
        }

        [HttpGet("contrato/{contratoId}")]
        public async Task<IActionResult> ListarPorContrato(int contratoId)
        {
            return Ok(await _service.ListarPorContratoAsync(contratoId));
        }

        [HttpPost]
        public async Task<IActionResult> Crear(CrearContactoRequest request)
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
    }
}
