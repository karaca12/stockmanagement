using StockManagement.Application.DTOs.Requests;
using StockManagement.Application.DTOs.Responses;

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
    }
}
