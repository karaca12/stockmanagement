using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StockManagement.Domain.Core.Entities;
using StockManagement.Domain.Core.Repositories;
using StockManagement.Domain.Entities;
using StockManagement.Domain.Interfaces.Repositories;
using StockManagement.Infrastructure.Data;

namespace StockManagement.Infrastructure.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly ApplicationDbContext _context;

        public CustomerRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IBaseRepositoryAsync<Customer> Repository<T>() where T : BaseEntity
        {
            throw new NotImplementedException();
        }
    }
}
