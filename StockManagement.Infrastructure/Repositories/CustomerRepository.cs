using StockManagement.Domain.Entities;
using StockManagement.Domain.Repositories;
using StockManagement.Infrastructure.Data;

namespace StockManagement.Infrastructure.Repositories
{
	public class CustomerRepository : BaseRepositoryAsync<Customer>, ICustomerRepository
	{
		private readonly ApplicationDbContext _context;

		public CustomerRepository(ApplicationDbContext context) : base(context)
		{
			_context = context;
		}
	}
}
