using StockManagement.Application.DTOs.Requests;
using StockManagement.Application.DTOs.Responses;
using StockManagement.Domain.Entities;

namespace StockManagement.Application.Services.Abstract
{
    public interface ICategoryService
    {
        Task<Category> GetByIdAsync(int id);
        Task<IEnumerable<GetAllCategoriesResponse>> GetAllAsync();
        Task AddAsync(CreateCategoryRequest request);
        Task UpdateAsync(EditCategoryRequest request);
        Task DeleteAsync(int id);
        Task<bool> Exists(int id);
    }
}
