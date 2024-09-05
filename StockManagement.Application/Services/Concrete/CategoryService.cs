using StockManagement.Application.DTOs.Requests;
using StockManagement.Application.DTOs.Responses;
using StockManagement.Application.Services.Abstract;
using StockManagement.Domain.Entities;
using StockManagement.Domain.Repositories;

namespace StockManagement.Application.Services.Concrete
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryService(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task AddAsync(CreateCategoryRequest request)
        {
            var category = new Category
            {
                Name = request.Name
            };
            await _categoryRepository.AddAsync(category);
        }

        public async Task DeleteAsync(int id)
        {
            await _categoryRepository.DeleteAsync(id);
        }

        public async Task<bool> Exists(int id)
        {
            return await _categoryRepository.Exists(id);
        }

        public async Task<IEnumerable<GetAllCategoriesResponse>> GetAllAsync()
        {
            var categories = await _categoryRepository.GetAllAsync();
            var response = categories.Select(c => new GetAllCategoriesResponse
            {
                Id = c.Id,
                Name = c.Name
            });
            return response;
        }

        public async Task<Category> GetByIdAsync(int id)
        {
            return await _categoryRepository.GetByIdAsync(id);
        }

        public async Task UpdateAsync(EditCategoryRequest request)
        {
            var category = GetByIdAsync(request.Id).Result;
            category.Name = request.Name;
            await _categoryRepository.UpdateAsync(category);
        }
    }
}
