using StockManagement.Domain.Entities;
using StockManagement.Domain.Repositories;
using StockManagement.Infrastructure.Data;

namespace StockManagement.Infrastructure.Repositories
{
    public class OrderRepository : BaseRepositoryAsync<Order>, IOrderRepository
    {
        private readonly ApplicationDbContext _context;

        public OrderRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }
    }
}
