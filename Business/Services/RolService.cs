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
    public class RolService
    {
        private readonly IRolRepository _repository;

        public RolService(IRolRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<RolDTO>> GetAllAsync()
        {
            var roles = await _repository.GetAllAsync();
            return roles.Select(r => new RolDTO
            {
                RolId = r.RolId,
                Nombre = r.Nombre
            });
        }

        public async Task<RolDTO> GetByIdAsync(int id)
        {
            var rol = await _repository.GetByIdAsync(id);
            if (rol == null) return null;

            return new RolDTO
            {
                RolId = rol.RolId,
                Nombre = rol.Nombre
            };
        }

        public async Task AddAsync(RolDTO dto)
        {
            var rol = new Rol { Nombre = dto.Nombre };
            await _repository.AddAsync(rol);
        }

        public async Task UpdateAsync(RolDTO dto)
        {
            var rol = new Rol { RolId = dto.RolId, Nombre = dto.Nombre };
            await _repository.UpdateAsync(rol);
        }

        public async Task DeleteAsync(int id) => await _repository.DeleteAsync(id);
    }
}
