﻿using StockManagement.Domain.Core.Repositories;
using StockManagement.Domain.Entities;

namespace StockManagement.Domain.Repositories;

public interface ICustomerRepository : IBaseRepositoryAsync<Customer>
{
    Task<bool> ExistsByNameAndSurname(string name, string surname);
}