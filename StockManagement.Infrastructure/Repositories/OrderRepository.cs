using Microsoft.EntityFrameworkCore;
using StockManagement.Domain.Entities;
using StockManagement.Domain.Repositories;
using StockManagement.Infrastructure.Data;

namespace StockManagement.Infrastructure.Repositories;

public class OrderRepository : BaseRepositoryAsync<Order>, IOrderRepository
{
    private readonly ApplicationDbContext _context;
    private readonly DbSet<Order> _orders;

    public OrderRepository(ApplicationDbContext context) : base(context)
    {
        _context = context;
        _orders = _context.Orders;
    }

    public async Task<IEnumerable<Order>> GetAllWithCustomerAndProductAsync()
    {
        return await _orders.Where(o => !o.IsDeleted)
            .Include(o => o.Product)
            .Include(o => o.Customer)
            .ToListAsync();
    }

    public async Task<Order> GetByIdWithCustomerAndProductAsync(int id)
    {
        return await _orders
            .Include(o => o.Product)
            .Include(o => o.Customer)
            .FirstOrDefaultAsync(p => p.Id == id && !p.IsDeleted);
    }
}