using StockManagement.Domain.Core.Entities;

namespace StockManagement.Domain.Entities;

public class Category : BaseEntity
{
    public string Name { get; set; }
    public ICollection<Product> Products { get; set; }
}