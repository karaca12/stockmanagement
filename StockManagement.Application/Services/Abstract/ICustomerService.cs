using StockManagement.Application.ViewModels.Requests;
using StockManagement.Application.ViewModels.Responses;
using StockManagement.Domain.Core.Paging;

namespace StockManagement.Application.Services.Abstract
{
	public interface ICustomerService
	{
		Task<GetCustomerByIdViewModel> GetByIdAsync(int id);
		Task<IEnumerable<GetAllCustomersViewModel>> GetAllAsync();
		Task AddAsync(CreateCustomerViewModel request);
		Task UpdateAsync(EditCustomerViewModel request);
		Task DeleteAsync(int id);
		Task<bool> Exists(int id);
		Task<PagedList<GetAllCustomersViewModel>> GetAllPagedAsync(int pageNumber, int pageSize, string searchString);
	}
}
