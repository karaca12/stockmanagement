using StockManagement.Application.ViewModels.Requests;
using StockManagement.Application.ViewModels.Responses;
using StockManagement.Domain.Core.Paging;

namespace StockManagement.Application.Services.Abstract
{
	public interface IProductService
	{
		Task<GetProductByIdWithCategoryViewModel> GetByIdWithCategoryAsync(int id);
		Task<IEnumerable<GetAllProductsWithCategoryViewModel>> GetAllWithCategoryAsync();
		Task AddAsync(CreateProductViewModel request);
		Task UpdateAsync(EditProductViewModel request);
		Task DeleteAsync(int id);
		Task<IEnumerable<GetAllCategoriesViewModel>> GetAllCategoriesAsync();
		Task<bool> Exists(int id);
		Task<IEnumerable<GetAllProductsViewModel>> GetAllAsync();
		Task<PagedList<GetAllProductsWithCategoryViewModel>> GetAllWithCategoryPagedAsync(int pageNumber, int pageSize, string searchString);

	}
}
