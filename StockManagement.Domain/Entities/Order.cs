using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StockManagement.Domain.Core.Entities;

namespace StockManagement.Domain.Entities
{
    public class Order:BaseEntity
    {
        public int ProductId { get; set; }
        public int CustomerId { get; set; }
        public int Pieces { get; set; }
        public decimal Price { get; set; }
        public Product Product { get; set; }
        public Customer Customer { get; set; }
    }
}
