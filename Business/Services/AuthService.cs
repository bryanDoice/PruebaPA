using Business.DTOs;
using DataAccess.Repositories.Interfaces;
using Entities.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services
{
    public class AuthService
    {
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly PasswordHasher<Usuario> _hasher;

        public AuthService(IUsuarioRepository usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
            _hasher = new PasswordHasher<Usuario>();
        }

        public async Task<UsuarioDTO> LoginAsync(string email, string plainPassword)
        {
            var usuario = await _usuarioRepository.GetByEmailAsync(email);
            if (usuario == null)
                throw new Exception("Correo no registrado.");

            var result = _hasher.VerifyHashedPassword(usuario, usuario.HashPassword, plainPassword);
            if (result == PasswordVerificationResult.Failed)
                throw new Exception("Contraseña incorrecta.");

            return new UsuarioDTO
            {
                UsuarioId = usuario.UsuarioId,
                Nombre = usuario.Nombre,
                Email = usuario.Email,
                RolId = usuario.RolId
            };
        }

        public async Task RegistrarUsuarioAsync(UsuarioDTO dto, string plainPassword)
        {
            var usuario = new Usuario
            {
                Nombre = dto.Nombre,
                Email = dto.Email,
                RolId = dto.RolId
            };

            usuario.HashPassword = _hasher.HashPassword(usuario, plainPassword);
            await _usuarioRepository.AddAsync(usuario);
        }
    }
}

