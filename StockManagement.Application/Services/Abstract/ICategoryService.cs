using StockManagement.Application.DTOs.Requests;
using StockManagement.Application.DTOs.Responses;
using StockManagement.Domain.Core.Paging;

namespace StockManagement.Application.Services.Abstract
{
	public interface ICategoryService
	{
		Task<GetCategoryByIdResponse> GetByIdAsync(int id);
		Task<IEnumerable<GetAllCategoriesResponse>> GetAllAsync();
		Task AddAsync(CreateCategoryRequest request);
		Task UpdateAsync(EditCategoryRequest request);
		Task DeleteAsync(int id);
		Task<bool> Exists(int id);
		Task<PagedList<GetAllCategoriesResponse>> GetAllPagedAsync(int pageNumber, int pageSize, string searchString);
	}
}
