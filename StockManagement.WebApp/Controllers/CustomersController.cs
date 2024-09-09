using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StockManagement.Application.Services.Abstract;
using StockManagement.Application.ViewModels.Requests;
using StockManagement.Application.ViewModels.Responses;

namespace StockManagement.Web.Controllers
{
	[Authorize]
	public class CustomersController : Controller
	{
		private readonly ICustomerService _customerService;

		public CustomersController(ICustomerService customerService)
		{
			_customerService = customerService;
		}

		public async Task<IActionResult> Index(string searchString, int pageNumber = 1, int pageSize = 10)
		{
			var pagedCustomers = await _customerService.GetAllPagedAsync(pageNumber, pageSize, searchString);
			ViewData["CurrentFilter"] = searchString;
			return View(pagedCustomers);
		}

		public IActionResult Create()
		{
			return View();
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Create(CreateCustomerViewModel request)
		{
			if (ModelState.IsValid)
			{
				try
				{
					await _customerService.AddAsync(request);
					return RedirectToAction(nameof(Index));
				}
				catch (InvalidOperationException ex)
				{
					ModelState.AddModelError(string.Empty, ex.Message);
				}
			}
			return View();
		}

		public async Task<IActionResult> Edit(int? id)
		{
			if (id == null)
			{
				return RedirectToAction("PageNotFound", "Error");
			}

			var customer = await _customerService.GetByIdAsync((int)id);
			if (customer == null)
			{
				return RedirectToAction("PageNotFound", "Error");
			}
			var editRequest = new EditCustomerViewModel
			{
				Id = customer.Id,
				Name = customer.Name,
				Surname = customer.Surname
			};
			return View(editRequest);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Edit(int id, EditCustomerViewModel request)
		{
			if (id != request.Id)
			{
				return RedirectToAction("PageNotFound", "Error");
			}

			if (ModelState.IsValid)
			{
				try
				{
					await _customerService.UpdateAsync(request);
					return RedirectToAction(nameof(Index));
				}
				catch (InvalidOperationException ex)
				{
					ModelState.AddModelError(string.Empty, ex.Message);
				}
				catch (DbUpdateConcurrencyException)
				{
					if (!await _customerService.Exists(request.Id))
					{
						return RedirectToAction("PageNotFound", "Error");
					}
					else
					{
						throw;
					}
				}
			}
			return View();
		}

		public async Task<IActionResult> Delete(int? id)
		{
			if (id == null)
			{
				return RedirectToAction("PageNotFound", "Error");
			}

			var customer = await _customerService.GetByIdAsync((int)id);
			if (customer == null)
			{
				return RedirectToAction("PageNotFound", "Error");
			}
			var deletedResponse = new DeleteCustomerViewModel
			{
				Id = customer.Id,
				Name = customer.Name,
				Surname = customer.Surname
			};

			return View(deletedResponse);
		}

		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> DeleteConfirmed(int? id)
		{
			if (id == null)
			{
				return RedirectToAction("PageNotFound", "Error");
			}

			await _customerService.DeleteAsync((int)id);
			return RedirectToAction(nameof(Index));
		}
	}
}
