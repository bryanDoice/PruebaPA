using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories.Interfaces
{
    public interface IAuditoriaRepository
    {
        Task<IEnumerable<Auditoria>> GetAllAsync();
        Task<Auditoria> GetByIdAsync(int id);
        Task AddAsync(Auditoria auditoria);
        Task UpdateAsync(Auditoria auditoria);
        Task DeleteAsync(int id);
    }
}
