using StockManagement.Application.DTOs.Requests;
using StockManagement.Application.DTOs.Responses;

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
    }
}
