using StockManagement.Application.DTOs.Requests;
using StockManagement.Application.DTOs.Responses;

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
    }
}
