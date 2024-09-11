using Microsoft.EntityFrameworkCore;
using StockManagement.Domain.Entities;
using StockManagement.Domain.Repositories;
using StockManagement.Infrastructure.Data;

namespace StockManagement.Infrastructure.Repositories;

public class CustomerRepository : BaseRepositoryAsync<Customer>, ICustomerRepository
{
    private readonly ApplicationDbContext _context;
    private readonly DbSet<Customer> _customers;

    public CustomerRepository(ApplicationDbContext context) : base(context)
    {
        _context = context;
        _customers = _context.Customers;
    }

    public async Task<bool> ExistsByNameAndSurname(string name, string surname)
    {
        return await _customers.AnyAsync(c => c.Name == name && c.Surname == surname);
    }
}