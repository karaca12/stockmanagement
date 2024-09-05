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
    public class OrderRepository : IOrderRepository
    {
        private readonly ApplicationDbContext _context;

        public OrderRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IBaseRepositoryAsync<Order> Repository<T>() where T : BaseEntity
        {
            throw new NotImplementedException();
        }
    }
}
