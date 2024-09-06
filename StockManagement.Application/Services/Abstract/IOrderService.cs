using StockManagement.Application.DTOs.Requests;
using StockManagement.Application.DTOs.Responses;
using StockManagement.Domain.Core.Paging;

namespace StockManagement.Application.Services.Abstract
{
	public interface IOrderService
	{
		Task<GetOrderByIdWithCustomerAndProductResponse> GetByIdWithCustomerAndProductAsync(int id);
		Task<IEnumerable<GetAllOrdersWithCustomerAndProductResponse>> GetAllWithCustomerAndProductAsync();
		Task AddAsync(CreateOrderRequest request);
		Task UpdateAsync(EditOrderRequest request);
		Task DeleteAsync(int id);
		Task<IEnumerable<GetAllCustomersResponse>> GetAllCustomersAsync();
		Task<IEnumerable<GetAllProductsResponse>> GetAllProductsAsync();
		Task<bool> Exists(int id);
		Task<PagedList<GetAllOrdersWithCustomerAndProductResponse>> GetAllWithCustomerAndProductPagedAsync(int pageNumber, int pageSize, string searchString);

	}
}
