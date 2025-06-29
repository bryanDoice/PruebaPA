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
    public class PrestamoService
    {

        private readonly IPrestamoRepository _repository;

        public PrestamoService(IPrestamoRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<PrestamoDTO>> GetAllAsync()
        {
            var prestamos = await _repository.GetAllAsync();
            return prestamos.Select(p => new PrestamoDTO
            {
                PrestamoId = p.PrestamoId,
                ArticuloId = p.ArticuloId,
                UsuarioSolicitaId = p.UsuarioSolicitaId,
                UsuarioApruebaId = p.UsuarioApruebaId,
                FechaSolicitud = p.FechaSolicitud,
                FechaEntrega = p.FechaEntrega,
                FechaDevolucion = p.FechaDevolucion,
                Estado = p.Estado
            });
        }

        public async Task<PrestamoDTO> GetByIdAsync(int id)
        {
            var p = await _repository.GetByIdAsync(id);
            if (p == null) return null;

            return new PrestamoDTO
            {
                PrestamoId = p.PrestamoId,
                ArticuloId = p.ArticuloId,
                UsuarioSolicitaId = p.UsuarioSolicitaId,
                UsuarioApruebaId = p.UsuarioApruebaId,
                FechaSolicitud = p.FechaSolicitud,
                FechaEntrega = p.FechaEntrega,
                FechaDevolucion = p.FechaDevolucion,
                Estado = p.Estado
            };
        }

        public async Task AddAsync(PrestamoDTO dto)
        {
            var p = new Prestamo
            {
                ArticuloId = dto.ArticuloId,
                UsuarioSolicitaId = dto.UsuarioSolicitaId,
                UsuarioApruebaId = dto.UsuarioApruebaId,
                FechaSolicitud = dto.FechaSolicitud,
                FechaEntrega = dto.FechaEntrega,
                FechaDevolucion = dto.FechaDevolucion,
                Estado = dto.Estado
            };
            await _repository.AddAsync(p);
        }

        public async Task UpdateAsync(PrestamoDTO dto)
        {
            var p = new Prestamo
            {
                PrestamoId = dto.PrestamoId,
                ArticuloId = dto.ArticuloId,
                UsuarioSolicitaId = dto.UsuarioSolicitaId,
                UsuarioApruebaId = dto.UsuarioApruebaId,
                FechaSolicitud = dto.FechaSolicitud,
                FechaEntrega = dto.FechaEntrega,
                FechaDevolucion = dto.FechaDevolucion,
                Estado = dto.Estado
            };
            await _repository.UpdateAsync(p);
        }

        public async Task DeleteAsync(int id)
            => await _repository.DeleteAsync(id);
    }
}
