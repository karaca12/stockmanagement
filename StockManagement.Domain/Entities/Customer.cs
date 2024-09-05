using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StockManagement.Domain.Core.Entities;

namespace StockManagement.Domain.Entities
{
    public class Customer:BaseEntity
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public ICollection<Order> Orders { get; set; }
    }
}
