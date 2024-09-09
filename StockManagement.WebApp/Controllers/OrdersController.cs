using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using StockManagement.Application.Services.Abstract;
using StockManagement.Application.ViewModels.Requests;
using StockManagement.Application.ViewModels.Responses;

namespace StockManagement.Web.Controllers
{
	public class OrdersController : Controller
	{
		private readonly IOrderService _orderService;

		public OrdersController(IOrderService orderService)
		{
			_orderService = orderService;
		}

		public async Task<IActionResult> Index(string searchString, int pageNumber = 1, int pageSize = 10)
		{
			var pagedOrders = await _orderService.GetAllWithCustomerAndProductPagedAsync(pageNumber, pageSize, searchString);
			ViewData["CurrentFilter"] = searchString;
			return View(pagedOrders);
		}

		public IActionResult Create()
		{
			ViewData["CustomerId"] = new SelectList(_orderService.GetAllCustomersAsync().Result, "Id", "Name");
			ViewData["ProductId"] = new SelectList(_orderService.GetAllProductsAsync().Result, "Id", "Name");
			return View();
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Create(CreateOrderViewModel request)
		{
			if (ModelState.IsValid)
			{
				try
				{
					await _orderService.AddAsync(request);
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

			var order = await _orderService.GetByIdWithCustomerAndProductAsync((int)id);
			if (order == null)
			{
				return RedirectToAction("PageNotFound", "Error");
			}
			var editRequest = new EditOrderViewModel
			{
				Id = order.Id,
				ProductId = order.ProductId,
				CustomerId = order.CustomerId,
				Pieces = order.Pieces,
				Price = order.Price
			};

			ViewData["CustomerId"] = new SelectList(_orderService.GetAllCustomersAsync().Result, "Id", "Name", order.CustomerId);
			ViewData["ProductId"] = new SelectList(_orderService.GetAllProductsAsync().Result, "Id", "Name", order.ProductId);
			return View(editRequest);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Edit(int id, EditOrderViewModel request)
		{
			if (id != request.Id)
			{
				return RedirectToAction("PageNotFound", "Error");
			}

			if (ModelState.IsValid)
			{
				try
				{
					await _orderService.UpdateAsync(request);
				}
				catch (DbUpdateConcurrencyException)
				{
					if (!await _orderService.Exists(request.Id))
					{
						return RedirectToAction("PageNotFound", "Error");
					}
					else
					{
						throw;
					}
				}
				return RedirectToAction(nameof(Index));
			}
			return View(id);
		}

		public async Task<IActionResult> Delete(int? id)
		{
			if (id == null)
			{
				return RedirectToAction("PageNotFound", "Error");
			}

			var order = await _orderService.GetByIdWithCustomerAndProductAsync((int)id);
			if (order == null)
			{
				return RedirectToAction("PageNotFound", "Error");
			}
			var deleteResponse = new DeleteOrderViewModel
			{
				Id = order.Id,
				Product = order.ProductName,
				Customer = order.CustomerName,
				Pieces = order.Pieces,
				Price = order.Price
			};

			return View(deleteResponse);
		}

		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> DeleteConfirmed(int? id)
		{
			if (id == null)
			{
				return RedirectToAction("PageNotFound", "Error");
			}

			await _orderService.DeleteAsync((int)id);
			return RedirectToAction(nameof(Index));
		}
	}
}
