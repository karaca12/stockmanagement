﻿using StockManagement.Domain.Entities;

namespace StockManagement.Application.Services.Abstract
{
    public interface ICustomerService
    {
        Task<Customer> GetByIdAsync(int id);
        Task<IEnumerable<Customer>> GetAllAsync();
        Task AddAsync(Customer customer);
        Task UpdateAsync(Customer customer);
        Task DeleteAsync(int id);
    }
}
