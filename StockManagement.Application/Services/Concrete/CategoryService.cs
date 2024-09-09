using StockManagement.Application.Services.Abstract;
using StockManagement.Application.ViewModels.Requests;
using StockManagement.Application.ViewModels.Responses;
using StockManagement.Domain.Core.Paging;
using StockManagement.Domain.Entities;
using StockManagement.Domain.Repositories;

namespace StockManagement.Application.Services.Concrete
{
	public class CategoryService : ICategoryService
	{
		private readonly ICategoryRepository _categoryRepository;

		public CategoryService(ICategoryRepository categoryRepository)
		{
			_categoryRepository = categoryRepository;
		}

		public async Task AddAsync(CreateCategoryViewModel request)
		{
			if (await _categoryRepository.ExistsByName(request.Name))
			{
				throw new InvalidOperationException("Category with the same name already exists.");
			}

			var category = new Category
			{
				Name = request.Name
			};
			await _categoryRepository.AddAsync(category);
		}

		public async Task DeleteAsync(int id)
		{
			await _categoryRepository.DeleteAsync(id);
		}

		public async Task<bool> Exists(int id)
		{
			return await _categoryRepository.Exists(id);
		}

		public async Task<IEnumerable<GetAllCategoriesViewModel>> GetAllAsync()
		{
			var categories = await _categoryRepository.GetAllAsync();
			var response = categories.Select(c => new GetAllCategoriesViewModel
			{
				Id = c.Id,
				Name = c.Name
			});
			return response;
		}

		public async Task<PagedList<GetAllCategoriesViewModel>> GetAllPagedAsync(int pageNumber, int pageSize, string searchString = null)
		{
			var categories = await _categoryRepository.GetAllAsync();
			if (!string.IsNullOrEmpty(searchString))
			{
				categories = categories.Where(c => c.Name.Contains(searchString, StringComparison.OrdinalIgnoreCase));
			}

			var response = categories.Select(c => new GetAllCategoriesViewModel
			{
				Id = c.Id,
				Name = c.Name
			}).ToList();
			return PagedList<GetAllCategoriesViewModel>.Create(response, pageNumber, pageSize);
		}

		public async Task<GetCategoryByIdViewModel> GetByIdAsync(int id)
		{
			var category = await _categoryRepository.GetByIdAsync(id);
			var response = new GetCategoryByIdViewModel
			{
				Id = category.Id,
				Name = category.Name
			};
			return response;
		}

		public async Task UpdateAsync(EditCategoryViewModel request)
		{
			var category = _categoryRepository.GetByIdAsync(request.Id).Result;

			if (await _categoryRepository.ExistsByName(request.Name) && category.Name != request.Name)
			{
				throw new InvalidOperationException("Category with the same name already exists.");
			}

			category.Name = request.Name;
			await _categoryRepository.UpdateAsync(category);
		}
	}
}
