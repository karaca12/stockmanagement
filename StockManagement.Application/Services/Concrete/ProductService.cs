using StockManagement.Application.DTOs.Requests;
using StockManagement.Application.DTOs.Responses;
using StockManagement.Application.Services.Abstract;
using StockManagement.Domain.Entities;
using StockManagement.Domain.Repositories;

namespace StockManagement.Application.Services.Concrete
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly ICategoryService _categoryService;

        public ProductService(IProductRepository productRepository, ICategoryService categoryService)
        {
            _productRepository = productRepository;
            _categoryService = categoryService;
        }

        public async Task AddAsync(CreateProductRequest request)
        {
            var product = new Product
            {
                Name = request.Name,
                Brand = request.Brand,
                CategoryId = request.CategoryId,
                Price = request.Price,
                Stock = request.Stock
            };
            await _productRepository.AddAsync(product);
        }

        public async Task DeleteAsync(int id)
        {
            await _productRepository.DeleteAsync(id);
        }

        public async Task<bool> Exists(int id)
        {
            return await _productRepository.Exists(id);
        }

        public async Task<IEnumerable<GetAllProductsWithCategoryResponse>> GetAllWithCategoryAsync()
        {
            var products = await _productRepository.GetAllWithCategoryAsync();
            var response = products.Select(p => new GetAllProductsWithCategoryResponse
            {
                Id = p.Id,
                Name = p.Name,
                Brand = p.Brand,
                Category = p.Category.Name,
                Price = p.Price,
                Stock = p.Stock
            });
            return response;
        }

        public async Task<IEnumerable<GetAllCategoriesResponse>> GetAllCategoriesAsync()
        {
            return await _categoryService.GetAllAsync();
        }

        public async Task<GetProductByIdWithCategoryResponse> GetByIdWithCategoryAsync(int id)
        {
            var product = await _productRepository.GetByIdWithCategoryAsync(id);
            var response = new GetProductByIdWithCategoryResponse
            {
                Id = product.Id,
                Name = product.Name,
                Brand = product.Brand,
                CategoryId = product.CategoryId,
                CategoryName = product.Category.Name,
                Price = product.Price,
                Stock = product.Stock
            };
            return response;
        }

        public async Task UpdateAsync(EditProductRequest request)
        {
            var product = _productRepository.GetByIdWithCategoryAsync(request.Id).Result;
            product.Name = request.Name;
            product.Brand = request.Brand;
            product.CategoryId = request.CategoryId;
            product.Price = request.Price;
            product.Stock = request.Stock;
            await _productRepository.UpdateAsync(product);
        }

        public async Task<IEnumerable<GetAllProductsResponse>> GetAllAsync()
        {
            var products = await _productRepository.GetAllAsync();
            var response = products.Select(p => new GetAllProductsResponse
            {
                Id = p.Id,
                Name = p.Name,
                Brand = p.Brand,
                Price = p.Price,
                Stock = p.Stock
            });
            return response;
        }
    }
}
