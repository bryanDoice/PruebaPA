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
    public class ArticuloService
    {
        private readonly IArticuloRepository _repository;

        public ArticuloService(IArticuloRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<ArticuloDTO>> GetAllAsync()
        {
            var articulos = await _repository.GetAllAsync();
            return articulos.Select(a => new ArticuloDTO
            {
                ArticuloId = a.ArticuloId,
                Codigo = a.Codigo,
                Nombre = a.Nombre,
                Estado = a.Estado,
                Ubicacion = a.Ubicacion,
                CategoriaId = a.CategoriaId
            });
        }

        public async Task<ArticuloDTO> GetByIdAsync(int id)
        {
            var articulo = await _repository.GetByIdAsync(id);
            if (articulo == null) return null;

            return new ArticuloDTO
            {
                ArticuloId = articulo.ArticuloId,
                Codigo = articulo.Codigo,
                Nombre = articulo.Nombre,
                Estado = articulo.Estado,
                Ubicacion = articulo.Ubicacion,
                CategoriaId = articulo.CategoriaId
            };
        }

        public async Task AddAsync(ArticuloDTO dto)
        {
            var articulo = new Articulo
            {
                Codigo = dto.Codigo,
                Nombre = dto.Nombre,
                Estado = dto.Estado,
                Ubicacion = dto.Ubicacion,
                CategoriaId = dto.CategoriaId
            };
            await _repository.AddAsync(articulo);
        }

        public async Task UpdateAsync(ArticuloDTO dto)
        {
            var articulo = new Articulo
            {
                ArticuloId = dto.ArticuloId,
                Codigo = dto.Codigo,
                Nombre = dto.Nombre,
                Estado = dto.Estado,
                Ubicacion = dto.Ubicacion,
                CategoriaId = dto.CategoriaId
            };
            await _repository.UpdateAsync(articulo);
        }

        public async Task DeleteAsync(int id)
        {
            await _repository.DeleteAsync(id);
        }
    }
}
