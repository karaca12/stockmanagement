using StockManagement.Application.Services.Abstract;
using StockManagement.Domain.Core.Paging;
using StockManagement.Domain.Entities;
using StockManagement.Domain.Repositories;
using StockManagement.Domain.ViewModels.Requests;
using StockManagement.Domain.ViewModels.Responses;

namespace StockManagement.Application.Services.Concrete
{
	public class CustomerService : ICustomerService
	{
		private readonly ICustomerRepository _customerRepository;

		public CustomerService(ICustomerRepository customerRepository)
		{
			_customerRepository = customerRepository;
		}

		public async Task AddAsync(CreateCustomerViewModel request)
		{
			if (await _customerRepository.ExistsByNameAndSurname(request.Name, request.Surname))
			{
				throw new InvalidOperationException("Customer already exists.");
			}

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

		public async Task<IEnumerable<GetAllCustomersViewModel>> GetAllAsync()
		{
			var customers = await _customerRepository.GetAllAsync();
			var response = customers.Select(c => new GetAllCustomersViewModel
			{
				Id = c.Id,
				Name = c.Name,
				Surname = c.Surname
			});
			return response;
		}

		public async Task<PagedList<GetAllCustomersViewModel>> GetAllPagedAsync(int pageNumber, int pageSize, string searchString = null)
		{
			var customers = await _customerRepository.GetAllAsync();
			if (!string.IsNullOrEmpty(searchString))
			{
				customers = customers.Where(c => c.Name.Contains(searchString, StringComparison.OrdinalIgnoreCase)
				|| c.Surname.Contains(searchString, StringComparison.OrdinalIgnoreCase));
			}

			var response = customers.Select(c => new GetAllCustomersViewModel
			{
				Id = c.Id,
				Name = c.Name,
				Surname = c.Surname
			}).ToList();
			return PagedList<GetAllCustomersViewModel>.Create(response, pageNumber, pageSize);
		}

		public async Task<GetCustomerByIdViewModel> GetByIdAsync(int id)
		{
			var customer = await _customerRepository.GetByIdAsync(id);
			var response = new GetCustomerByIdViewModel
			{
				Id = customer.Id,
				Name = customer.Name,
				Surname = customer.Surname
			};
			return response;
		}

		public async Task UpdateAsync(EditCustomerViewModel request)
		{
			var customer = _customerRepository.GetByIdAsync(request.Id).Result;

			if (await _customerRepository.ExistsByNameAndSurname(request.Name, request.Surname)
				&& (customer.Name != request.Name || customer.Surname != request.Surname))
			{
				throw new InvalidOperationException("Customer already exists.");
			}

			customer.Name = request.Name;
			customer.Surname = request.Surname;
			await _customerRepository.UpdateAsync(customer);
		}
	}
}
