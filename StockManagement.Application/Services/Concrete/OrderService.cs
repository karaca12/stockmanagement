﻿using StockManagement.Application.Services.Abstract;
using StockManagement.Domain.Core.Paging;
using StockManagement.Domain.Entities;
using StockManagement.Domain.Repositories;
using StockManagement.Domain.ViewModels.Requests;
using StockManagement.Domain.ViewModels.Responses;

namespace StockManagement.Application.Services.Concrete;

public class OrderService : IOrderService
{
    private readonly ICustomerService _customerService;
    private readonly IOrderRepository _orderRepository;
    private readonly IProductService _productService;

    public OrderService(IOrderRepository orderRepository, IProductService productService,
        ICustomerService customerService)
    {
        _orderRepository = orderRepository;
        _productService = productService;
        _customerService = customerService;
    }

    public async Task AddAsync(CreateOrderViewModel request)
    {
        var order = new Order
        {
            ProductId = request.ProductId,
            CustomerId = request.CustomerId,
            Pieces = request.Pieces,
            Price = request.Price
        };
        await _orderRepository.AddAsync(order);
    }

    public async Task DeleteAsync(int id)
    {
        await _orderRepository.DeleteAsync(id);
    }

    public async Task<bool> Exists(int id)
    {
        return await _orderRepository.Exists(id);
    }

    public async Task<IEnumerable<GetAllCustomersViewModel>> GetAllCustomersAsync()
    {
        return await _customerService.GetAllAsync();
    }

    public async Task<IEnumerable<GetAllProductsViewModel>> GetAllProductsAsync()
    {
        return await _productService.GetAllAsync();
    }

    public async Task<IEnumerable<GetAllOrdersWithCustomerAndProductViewModel>> GetAllWithCustomerAndProductAsync()
    {
        var orders = await _orderRepository.GetAllWithCustomerAndProductAsync();
        var response = orders.Select(o => new GetAllOrdersWithCustomerAndProductViewModel
        {
            Id = o.Id,
            Product = o.Product.Name,
            Customer = o.Customer.Name + " " + o.Customer.Surname,
            Pieces = o.Pieces,
            Price = o.Price
        });
        return response;
    }

    public async Task<PagedList<GetAllOrdersWithCustomerAndProductViewModel>> GetAllWithCustomerAndProductPagedAsync(
        int pageNumber, int pageSize, string searchString)
    {
        var orders = await _orderRepository.GetAllWithCustomerAndProductAsync();
        if (!string.IsNullOrEmpty(searchString))
            orders = orders.Where(o => o.Product.Name.Contains(searchString, StringComparison.OrdinalIgnoreCase)
                                       || o.Customer.Name.Contains(searchString, StringComparison.OrdinalIgnoreCase)
                                       || o.Customer.Surname.Contains(searchString,
                                           StringComparison.OrdinalIgnoreCase));

        var response = orders.Select(o => new GetAllOrdersWithCustomerAndProductViewModel
        {
            Id = o.Id,
            Product = o.Product.Name,
            Customer = o.Customer.Name + " " + o.Customer.Surname,
            Pieces = o.Pieces,
            Price = o.Price
        }).ToList();
        return PagedList<GetAllOrdersWithCustomerAndProductViewModel>.Create(response, pageNumber, pageSize);
    }

    public async Task<GetOrderByIdWithCustomerAndProductViewModel> GetByIdWithCustomerAndProductAsync(int id)
    {
        var order = await _orderRepository.GetByIdWithCustomerAndProductAsync(id);
        var response = new GetOrderByIdWithCustomerAndProductViewModel
        {
            Id = order.Id,
            ProductName = order.Product.Name,
            CustomerName = order.Customer.Name + " " + order.Customer.Surname,
            Pieces = order.Pieces,
            Price = order.Price
        };
        return response;
    }

    public async Task UpdateAsync(EditOrderViewModel request)
    {
        var order = _orderRepository.GetByIdWithCustomerAndProductAsync(request.Id).Result;
        order.ProductId = request.ProductId;
        order.CustomerId = request.CustomerId;
        order.Pieces = request.Pieces;
        order.Price = request.Price;
        await _orderRepository.UpdateAsync(order);
    }
}