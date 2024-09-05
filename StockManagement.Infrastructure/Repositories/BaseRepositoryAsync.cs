using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StockManagement.Domain.Core.Entities;
using StockManagement.Domain.Core.Repositories;
using StockManagement.Infrastructure.Data;

namespace StockManagement.Infrastructure.Repositories
{
    public class BaseRepositoryAsync<T> : IBaseRepositoryAsync<T> where T : BaseEntity
    {
        private readonly ApplicationDbContext _context;

        public BaseRepositoryAsync(ApplicationDbContext context)
        {
            _context = context;
        }

        public Task AddAsync(T entity)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<T>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<T> GetByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(T entity)
        {
            throw new NotImplementedException();
        }
    }
}
