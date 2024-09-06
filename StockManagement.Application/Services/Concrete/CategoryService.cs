using StockManagement.Application.DTOs.Requests;
using StockManagement.Application.DTOs.Responses;
using StockManagement.Application.Services.Abstract;
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

		public async Task AddAsync(CreateCategoryRequest request)
		{
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

		public async Task<IEnumerable<GetAllCategoriesResponse>> GetAllAsync()
		{
			var categories = await _categoryRepository.GetAllAsync();
			var response = categories.Select(c => new GetAllCategoriesResponse
			{
				Id = c.Id,
				Name = c.Name
			});
			return response;
		}

		public async Task<PagedList<GetAllCategoriesResponse>> GetAllPagedAsync(int pageNumber, int pageSize, string searchString = null)
		{
			var categories = await _categoryRepository.GetAllAsync();
			if (!string.IsNullOrEmpty(searchString))
			{
				categories = categories.Where(c => c.Name.Contains(searchString, StringComparison.OrdinalIgnoreCase));
			}

			var response = categories.Select(c => new GetAllCategoriesResponse
			{
				Id = c.Id,
				Name = c.Name
			}).ToList();
			return PagedList<GetAllCategoriesResponse>.Create(response, pageNumber, pageSize);
		}

		public async Task<GetCategoryByIdResponse> GetByIdAsync(int id)
		{
			var category = await _categoryRepository.GetByIdAsync(id);
			var response = new GetCategoryByIdResponse
			{
				Id = category.Id,
				Name = category.Name
			};
			return response;
		}

		public async Task UpdateAsync(EditCategoryRequest request)
		{
			var category = _categoryRepository.GetByIdAsync(request.Id).Result;
			category.Name = request.Name;
			await _categoryRepository.UpdateAsync(category);
		}
	}
}
