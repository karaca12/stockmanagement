using StockManagement.Domain.Core.Entities;

namespace StockManagement.Domain.Entities;

public class Customer : BaseEntity
{
    public string Name { get; set; }
    public string Surname { get; set; }
    public ICollection<Order> Orders { get; set; }
}