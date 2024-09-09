using Microsoft.EntityFrameworkCore;
using StockManagement.Domain.Entities;
using StockManagement.Domain.Repositories;
using StockManagement.Infrastructure.Data;

namespace StockManagement.Infrastructure.Repositories
{
	public class ProductRepository : BaseRepositoryAsync<Product>, IProductRepository
	{
		private readonly ApplicationDbContext _context;
		private readonly DbSet<Product> _products;

		public ProductRepository(ApplicationDbContext context) : base(context)
		{
			_context = context;
			_products = _context.Products;
		}

		public async Task<IEnumerable<Product>> GetAllWithCategoryAsync()
		{
			return await _products.Where(p => !p.IsDeleted)
				.Include(p => p.Category).ToListAsync();
		}


		public async Task<Product> GetByIdWithCategoryAsync(int id)
		{
			return await _products
				.Include(p => p.Category)
				.FirstOrDefaultAsync(p => p.Id == id && !p.IsDeleted);

		}

		public async Task<bool> ExistsByName(string name)
		{
			return await _products.AnyAsync(p => p.Name == name);
		}
	}
}
