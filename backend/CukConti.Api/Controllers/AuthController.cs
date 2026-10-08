using CukConti.Application.DTOs;
using CukConti.Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;

namespace CukConti.Api.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly AutenticacionService _autenticacionService;

        public AuthController(AutenticacionService autenticacionService)
        {
            _autenticacionService = autenticacionService;
        }

        [HttpPost("login")]
        [AllowAnonymous]
        [EnableRateLimiting("LoginPolicy")]
        public async Task<IActionResult> Login(LoginRequest request)
        {
            var resultado = await _autenticacionService.LoginAsync(request);

            if (resultado is null)
                return Unauthorized(new { mensaje = "Email o contraseña incorrectos." });

            return Ok(resultado);
        }

        [HttpPost("usuarios")]
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> CrearUsuario(CrearUsuarioRequest request)
        {
            try
            {
                var usuario = await _autenticacionService.CrearUsuarioAsync(request);
                return Ok(new { usuario.Id, usuario.Nombre, usuario.Email, Rol = usuario.Rol.ToString() });
            }
            catch (Exception ex)
            {
                return BadRequest(new { mensaje = ex.Message });
            }
        }
    }
}