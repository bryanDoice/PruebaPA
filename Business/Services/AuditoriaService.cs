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
    public class AuditoriaService
    {
        private readonly IAuditoriaRepository _repository;

        public AuditoriaService(IAuditoriaRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<AuditoriaDTO>> GetAllAsync()
        {
            var registros = await _repository.GetAllAsync();
            return registros.Select(a => new AuditoriaDTO
            {
                AuditoriaId = a.AuditoriaId,
                UsuarioId = a.UsuarioId,
                Accion = a.Accion,
                Fecha = a.Fecha,
                Entidad = a.Entidad,
                EntidadId = a.EntidadId,
                Antes = a.Antes,
                Despues = a.Despues
            });
        }

        public async Task<AuditoriaDTO> GetByIdAsync(int id)
        {
            var a = await _repository.GetByIdAsync(id);
            if (a == null) return null;

            return new AuditoriaDTO
            {
                AuditoriaId = a.AuditoriaId,
                UsuarioId = a.UsuarioId,
                Accion = a.Accion,
                Fecha = a.Fecha,
                Entidad = a.Entidad,
                EntidadId = a.EntidadId,
                Antes = a.Antes,
                Despues = a.Despues
            };
        }

        public async Task AddAsync(AuditoriaDTO dto)
        {
            var a = new Auditoria
            {
                UsuarioId = dto.UsuarioId,
                Accion = dto.Accion,
                Fecha = dto.Fecha,
                Entidad = dto.Entidad,
                EntidadId = dto.EntidadId,
                Antes = dto.Antes,
                Despues = dto.Despues
            };
            await _repository.AddAsync(a);
        }

        public async Task UpdateAsync(AuditoriaDTO dto)
        {
            var a = new Auditoria
            {
                AuditoriaId = dto.AuditoriaId,
                UsuarioId = dto.UsuarioId,
                Accion = dto.Accion,
                Fecha = dto.Fecha,
                Entidad = dto.Entidad,
                EntidadId = dto.EntidadId,
                Antes = dto.Antes,
                Despues = dto.Despues
            };
            await _repository.UpdateAsync(a);
        }

        public async Task DeleteAsync(int id) => await _repository.DeleteAsync(id);
    }
}
