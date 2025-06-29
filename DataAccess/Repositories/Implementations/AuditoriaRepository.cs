using DataAccess.Context;
using DataAccess.Repositories.Interfaces;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories.Implementations
{
    public class AuditoriaRepository : IAuditoriaRepository
    {
        private readonly InventarioDbContext _context;

        public AuditoriaRepository(InventarioDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Auditoria>> GetAllAsync()
            => await _context.Auditoria.ToListAsync();

        public async Task<Auditoria> GetByIdAsync(int id)
            => await _context.Auditoria.FindAsync(id);

        public async Task AddAsync(Auditoria auditoria)
        {
            _context.Auditoria.Add(auditoria);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Auditoria auditoria)
        {
            _context.Auditoria.Update(auditoria);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var auditoria = await _context.Auditoria.FindAsync(id);
            if (auditoria != null)
            {
                _context.Auditoria.Remove(auditoria);
                await _context.SaveChangesAsync();
            }
        }
    }
}
