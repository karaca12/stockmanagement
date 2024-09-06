using StockManagement.Application.DTOs.Requests;
using StockManagement.Application.DTOs.Responses;
using StockManagement.Domain.Core.Paging;

namespace StockManagement.Application.Services.Abstract
{
	public interface IProductService
	{
		Task<GetProductByIdWithCategoryResponse> GetByIdWithCategoryAsync(int id);
		Task<IEnumerable<GetAllProductsWithCategoryResponse>> GetAllWithCategoryAsync();
		Task AddAsync(CreateProductRequest request);
		Task UpdateAsync(EditProductRequest request);
		Task DeleteAsync(int id);
		Task<IEnumerable<GetAllCategoriesResponse>> GetAllCategoriesAsync();
		Task<bool> Exists(int id);
		Task<IEnumerable<GetAllProductsResponse>> GetAllAsync();
		Task<PagedList<GetAllProductsWithCategoryResponse>> GetAllWithCategoryPagedAsync(int pageNumber, int pageSize, string searchString);

	}
}
