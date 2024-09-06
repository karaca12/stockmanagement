using StockManagement.Application.DTOs.Requests;
using StockManagement.Application.DTOs.Responses;
using StockManagement.Application.Services.Abstract;
using StockManagement.Domain.Core.Paging;
using StockManagement.Domain.Entities;
using StockManagement.Domain.Repositories;

namespace StockManagement.Application.Services.Concrete
{
	public class CustomerService : ICustomerService
	{
		private readonly ICustomerRepository _customerRepository;

		public CustomerService(ICustomerRepository customerRepository)
		{
			_customerRepository = customerRepository;
		}

		public async Task AddAsync(CreateCustomerRequest request)
		{
			var customer = new Customer
			{
				Name = request.Name,
				Surname = request.Surname
			};
			await _customerRepository.AddAsync(customer);
		}

		public async Task DeleteAsync(int id)
		{
			await _customerRepository.DeleteAsync(id);
		}

		public async Task<bool> Exists(int id)
		{
			return await _customerRepository.Exists(id);
		}

		public async Task<IEnumerable<GetAllCustomersResponse>> GetAllAsync()
		{
			var customers = await _customerRepository.GetAllAsync();
			var response = customers.Select(c => new GetAllCustomersResponse
			{
				Id = c.Id,
				Name = c.Name,
				Surname = c.Surname
			});
			return response;
		}

		public async Task<PagedList<GetAllCustomersResponse>> GetAllPagedAsync(int pageNumber, int pageSize, string searchString = null)
		{
			var customers = await _customerRepository.GetAllAsync();
			if (!string.IsNullOrEmpty(searchString))
			{
				customers = customers.Where(c => c.Name.Contains(searchString, StringComparison.OrdinalIgnoreCase)
				|| c.Surname.Contains(searchString, StringComparison.OrdinalIgnoreCase));
			}

			var response = customers.Select(c => new GetAllCustomersResponse
			{
				Id = c.Id,
				Name = c.Name,
				Surname = c.Surname
			}).ToList();
			return PagedList<GetAllCustomersResponse>.Create(response, pageNumber, pageSize);
		}

		public async Task<GetCustomerByIdResponse> GetByIdAsync(int id)
		{
			var customer = await _customerRepository.GetByIdAsync(id);
			var response = new GetCustomerByIdResponse
			{
				Id = customer.Id,
				Name = customer.Name,
				Surname = customer.Surname
			};
			return response;
		}

		public async Task UpdateAsync(EditCustomerRequest request)
		{
			var customer = _customerRepository.GetByIdAsync(request.Id).Result;
			customer.Name = request.Name;
			customer.Surname = request.Surname;
			await _customerRepository.UpdateAsync(customer);
		}
	}
}
