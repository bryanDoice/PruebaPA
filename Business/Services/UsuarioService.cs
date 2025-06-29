using Business.DTOs;
using DataAccess.Repositories.Interfaces;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services
{
    public class UsuarioService
    {
        private readonly IUsuarioRepository _repository;

        public UsuarioService(IUsuarioRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<UsuarioDTO>> GetAllAsync()
        {
            var usuarios = await _repository.GetAllAsync();
            return usuarios.Select(u => new UsuarioDTO
            {
                UsuarioId = u.UsuarioId,
                Nombre = u.Nombre,
                Email = u.Email,
                HashPassword = u.HashPassword,
                RolId = u.RolId
            });
        }

        public async Task<UsuarioDTO> GetByIdAsync(int id)
        {
            var usuario = await _repository.GetByIdAsync(id);
            if (usuario == null) return null;

            return new UsuarioDTO
            {
                UsuarioId = usuario.UsuarioId,
                Nombre = usuario.Nombre,
                Email = usuario.Email,
                HashPassword = usuario.HashPassword,
                RolId = usuario.RolId
            };
        }

        public async Task AddAsync(UsuarioDTO dto)
        {
            var usuario = new Usuario
            {
                Nombre = dto.Nombre,
                Email = dto.Email,
                HashPassword = dto.HashPassword,
                RolId = dto.RolId
            };
            await _repository.AddAsync(usuario);
        }

        public async Task UpdateAsync(UsuarioDTO dto)
        {
            var usuario = new Usuario
            {
                UsuarioId = dto.UsuarioId,
                Nombre = dto.Nombre,
                Email = dto.Email,
                HashPassword = dto.HashPassword,
                RolId = dto.RolId
            };
            await _repository.UpdateAsync(usuario);
        }

        public async Task DeleteAsync(int id) => await _repository.DeleteAsync(id);
    }
}