using StockManagement.Application.ViewModels.Requests;
using StockManagement.Application.ViewModels.Responses;
using StockManagement.Domain.Core.Paging;

namespace StockManagement.Application.Services.Abstract
{
	public interface IOrderService
	{
		Task<GetOrderByIdWithCustomerAndProductViewModel> GetByIdWithCustomerAndProductAsync(int id);
		Task<IEnumerable<GetAllOrdersWithCustomerAndProductViewModel>> GetAllWithCustomerAndProductAsync();
		Task AddAsync(CreateOrderViewModel request);
		Task UpdateAsync(EditOrderViewModel request);
		Task DeleteAsync(int id);
		Task<IEnumerable<GetAllCustomersViewModel>> GetAllCustomersAsync();
		Task<IEnumerable<GetAllProductsViewModel>> GetAllProductsAsync();
		Task<bool> Exists(int id);
		Task<PagedList<GetAllOrdersWithCustomerAndProductViewModel>> GetAllWithCustomerAndProductPagedAsync(int pageNumber, int pageSize, string searchString);

	}
}
