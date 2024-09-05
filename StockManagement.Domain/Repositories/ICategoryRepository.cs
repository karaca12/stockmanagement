using StockManagement.Domain.Core.Repositories;
using StockManagement.Domain.Entities;

namespace StockManagement.Domain.Repositories
{
    public interface ICategoryRepository : IBaseRepositoryAsync<Category>
    {
    }
}
