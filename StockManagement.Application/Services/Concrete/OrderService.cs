using StockManagement.Application.Services.Abstract;
using StockManagement.Domain.Entities;
using StockManagement.Domain.Repositories;

namespace StockManagement.Application.Services.Concrete
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;

        public OrderService(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public Task AddAsync(Order order)
        {
            return _orderRepository.AddAsync(order);
        }

        public Task DeleteAsync(int id)
        {
            return _orderRepository.DeleteAsync(id);
        }

        public Task<IEnumerable<Order>> GetAllAsync()
        {
            return _orderRepository.GetAllAsync();
        }

        public Task<Order> GetByIdAsync(int id)
        {
            return _orderRepository.GetByIdAsync(id);
        }

        public Task UpdateAsync(Order order)
        {
            return _orderRepository.UpdateAsync(order);
        }
    }
}
