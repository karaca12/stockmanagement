using Microsoft.EntityFrameworkCore;
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

		public async Task<IEnumerable<Order>> GetAllWithCustomerAndProductAsync()
		{
			return await _context.Orders.Where(o => !o.IsDeleted)
				.Include(o => o.Product)
				.Include(o => o.Customer)
				.ToListAsync();
		}

		public async Task<Order> GetByIdWithCustomerAndProductAsync(int id)
		{
			return await _context.Orders
				.Include(o => o.Product)
				.Include(o => o.Customer)
				.FirstOrDefaultAsync(p => p.Id == id && !p.IsDeleted);
		}
	}
}
