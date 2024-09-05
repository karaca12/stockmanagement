using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StockManagement.Domain.Core.Entities;
using StockManagement.Domain.Core.Repositories;
using StockManagement.Domain.Entities;

namespace StockManagement.Domain.Interfaces.Repositories
{
    public interface IOrderRepository
    {
        IBaseRepositoryAsync<Order> Repository<T>() where T : BaseEntity;
    }
}
