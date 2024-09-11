using StockManagement.Application.Services.Abstract;
using StockManagement.Domain.Core.Paging;
using StockManagement.Domain.Entities;
using StockManagement.Domain.Repositories;
using StockManagement.Domain.ViewModels.Requests;
using StockManagement.Domain.ViewModels.Responses;

namespace StockManagement.Application.Services.Concrete;

public class ProductService : IProductService
{
    private readonly ICategoryService _categoryService;
    private readonly IProductRepository _productRepository;

    public ProductService(IProductRepository productRepository, ICategoryService categoryService)
    {
        _productRepository = productRepository;
        _categoryService = categoryService;
    }

    public async Task AddAsync(CreateProductViewModel request)
    {
        if (await _productRepository.ExistsByName(request.Name))
            throw new InvalidOperationException("Product with the same name already exists.");

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

    public async Task<IEnumerable<GetAllProductsWithCategoryViewModel>> GetAllWithCategoryAsync()
    {
        var products = await _productRepository.GetAllWithCategoryAsync();
        var response = products.Select(p => new GetAllProductsWithCategoryViewModel
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

    public async Task<IEnumerable<GetAllCategoriesViewModel>> GetAllCategoriesAsync()
    {
        return await _categoryService.GetAllAsync();
    }

    public async Task<GetProductByIdWithCategoryViewModel> GetByIdWithCategoryAsync(int id)
    {
        var product = await _productRepository.GetByIdWithCategoryAsync(id);
        var response = new GetProductByIdWithCategoryViewModel
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

    public async Task UpdateAsync(EditProductViewModel request)
    {
        var product = _productRepository.GetByIdWithCategoryAsync(request.Id).Result;

        if (await _productRepository.ExistsByName(request.Name) && product.Name != request.Name)
            throw new InvalidOperationException("Product with the same name already exists.");

        product.Name = request.Name;
        product.Brand = request.Brand;
        product.CategoryId = request.CategoryId;
        product.Price = request.Price;
        product.Stock = request.Stock;
        await _productRepository.UpdateAsync(product);
    }

    public async Task<IEnumerable<GetAllProductsViewModel>> GetAllAsync()
    {
        var products = await _productRepository.GetAllAsync();
        var response = products.Select(p => new GetAllProductsViewModel
        {
            Id = p.Id,
            Name = p.Name,
            Brand = p.Brand,
            Price = p.Price,
            Stock = p.Stock
        });
        return response;
    }

    public async Task<PagedList<GetAllProductsWithCategoryViewModel>> GetAllWithCategoryPagedAsync(int pageNumber,
        int pageSize, string searchString)
    {
        var products = await _productRepository.GetAllWithCategoryAsync();
        if (!string.IsNullOrEmpty(searchString))
            products = products.Where(p => p.Name.Contains(searchString, StringComparison.OrdinalIgnoreCase)
                                           || p.Brand.Contains(searchString, StringComparison.OrdinalIgnoreCase)
                                           || p.Category.Name.Contains(searchString,
                                               StringComparison.OrdinalIgnoreCase));

        var response = products.Select(p => new GetAllProductsWithCategoryViewModel
        {
            Id = p.Id,
            Name = p.Name,
            Brand = p.Brand,
            Category = p.Category.Name,
            Price = p.Price,
            Stock = p.Stock
        }).ToList();
        return PagedList<GetAllProductsWithCategoryViewModel>.Create(response, pageNumber, pageSize);
    }
}