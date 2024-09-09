using StockManagement.Application.ViewModels.Requests;
using StockManagement.Application.ViewModels.Responses;
using StockManagement.Domain.Core.Paging;

namespace StockManagement.Application.Services.Abstract
{
	public interface ICategoryService
	{
		Task<GetCategoryByIdViewModel> GetByIdAsync(int id);
		Task<IEnumerable<GetAllCategoriesViewModel>> GetAllAsync();
		Task AddAsync(CreateCategoryViewModel request);
		Task UpdateAsync(EditCategoryViewModel request);
		Task DeleteAsync(int id);
		Task<bool> Exists(int id);
		Task<PagedList<GetAllCategoriesViewModel>> GetAllPagedAsync(int pageNumber, int pageSize, string searchString);
	}
}
