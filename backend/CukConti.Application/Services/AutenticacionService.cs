using System;
using System.Threading.Tasks;
using CukConti.Application.DTOs;
using CukConti.Application.Interfaces;
using CukConti.Domain.Entities;
using CukConti.Domain.Enums;
using CukConti.Domain.Interfaces;

namespace CukConti.Application.Services
{
    public class AutenticacionService
    {
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IPasswordHasher _passwordHasher;
        private readonly IJwtTokenGenerator _jwtTokenGenerator;

        public AutenticacionService(
            IUsuarioRepository usuarioRepository,
            IPasswordHasher passwordHasher,
            IJwtTokenGenerator jwtTokenGenerator)
        {
            _usuarioRepository = usuarioRepository;
            _passwordHasher = passwordHasher;
            _jwtTokenGenerator = jwtTokenGenerator;
        }

        public async Task<LoginResponse?> LoginAsync(LoginRequest request)
        {
            var usuario = await _usuarioRepository.ObtenerPorEmailAsync(request.Email);

            if (usuario is null || !usuario.Activo)
                return null;

            if (!_passwordHasher.Verificar(request.Password, usuario.PasswordHash))
                return null;

            var token = _jwtTokenGenerator.GenerarToken(usuario);

            return new LoginResponse(token, usuario.Nombre, usuario.Rol.ToString());
        }

        public async Task<Usuario> CrearUsuarioAsync(CrearUsuarioRequest request)
        {
            var existente = await _usuarioRepository.ObtenerPorEmailAsync(request.Email);
            if (existente is not null)
                throw new InvalidOperationException("Ya existe un usuario con ese email.");

            if (!Enum.TryParse<RolUsuario>(request.Rol, true, out var rol))
                throw new ArgumentException("Rol invalido. Debe ser Administrador o Secretario.");

            var usuario = new Usuario
            {
                Nombre = request.Nombre,
                Email = request.Email,
                PasswordHash = _passwordHasher.Hashear(request.Password),
                Rol = rol,
                Activo = true
            };

            await _usuarioRepository.AgregarAsync(usuario);
            await _usuarioRepository.GuardarCambiosAsync();

            return usuario;
        }
    }
}
