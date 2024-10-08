﻿using StockManagement.Domain.Core.Repositories;
using StockManagement.Domain.Entities;

namespace StockManagement.Domain.Repositories;

public interface IProductRepository : IBaseRepositoryAsync<Product>
{
    public Task<IEnumerable<Product>> GetAllWithCategoryAsync();
    public Task<Product> GetByIdWithCategoryAsync(int id);
    public Task<bool> ExistsByName(string name);
}