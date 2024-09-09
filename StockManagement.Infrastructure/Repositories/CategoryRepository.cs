using Microsoft.EntityFrameworkCore;
using StockManagement.Domain.Entities;
using StockManagement.Domain.Repositories;
using StockManagement.Infrastructure.Data;

namespace StockManagement.Infrastructure.Repositories
{
	public class CategoryRepository : BaseRepositoryAsync<Category>, ICategoryRepository
	{
		private readonly ApplicationDbContext _context;
		private readonly DbSet<Category> _categories;

		public CategoryRepository(ApplicationDbContext context) : base(context)
		{
			_context = context;
			_categories = _context.Categories;
		}

		public async Task<bool> ExistsByName(string name)
		{
			return await _categories.AnyAsync(c => c.Name == name);
		}
	}
}
