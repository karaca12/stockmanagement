using StockManagement.Domain.Core.Repositories;
using StockManagement.Domain.Entities;

namespace StockManagement.Domain.Repositories
{
	public interface IOrderRepository : IBaseRepositoryAsync<Order>
	{
		Task<IEnumerable<Order>> GetAllWithCustomerAndProductAsync();
		Task<Order> GetByIdWithCustomerAndProductAsync(int id);
	}
}
