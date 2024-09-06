using StockManagement.Application.DTOs.Requests;
using StockManagement.Application.DTOs.Responses;
using StockManagement.Domain.Core.Paging;

namespace StockManagement.Application.Services.Abstract
{
	public interface ICustomerService
	{
		Task<GetCustomerByIdResponse> GetByIdAsync(int id);
		Task<IEnumerable<GetAllCustomersResponse>> GetAllAsync();
		Task AddAsync(CreateCustomerRequest request);
		Task UpdateAsync(EditCustomerRequest request);
		Task DeleteAsync(int id);
		Task<bool> Exists(int id);
		Task<PagedList<GetAllCustomersResponse>> GetAllPagedAsync(int pageNumber, int pageSize, string searchString);
	}
}
