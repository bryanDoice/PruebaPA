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
    public class CategoriaService
    {
        private readonly ICategoriaRepository _repository;

        public CategoriaService(ICategoriaRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<CategoriaDTO>> GetAllAsync()
        {
            var categorias = await _repository.GetAllAsync();
            return categorias.Select(c => new CategoriaDTO
            {
                CategoriaId = c.CategoriaId,
                Nombre = c.Nombre
            });
        }

        public async Task<CategoriaDTO> GetByIdAsync(int id)
        {
            var categoria = await _repository.GetByIdAsync(id);
            if (categoria == null) return null;

            return new CategoriaDTO
            {
                CategoriaId = categoria.CategoriaId,
                Nombre = categoria.Nombre
            };
        }

        public async Task AddAsync(CategoriaDTO dto)
        {
            var categoria = new Categoria { Nombre = dto.Nombre };
            await _repository.AddAsync(categoria);
        }

        public async Task UpdateAsync(CategoriaDTO dto)
        {
            var categoria = new Categoria
            {
                CategoriaId = dto.CategoriaId,
                Nombre = dto.Nombre
            };
            await _repository.UpdateAsync(categoria);
        }

        public async Task DeleteAsync(int id) => await _repository.DeleteAsync(id);
    }
}
