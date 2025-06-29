﻿using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories.Interfaces
{
    public interface IPrestamoRepository
    {
        Task<IEnumerable<Prestamo>> GetAllAsync();
        Task<Prestamo> GetByIdAsync(int id);
        Task AddAsync(Prestamo prestamo);
        Task UpdateAsync(Prestamo prestamo);
        Task DeleteAsync(int id);
    }
}