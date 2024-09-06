using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using StockManagement.Application.DTOs.Requests;
using StockManagement.Application.DTOs.Responses;
using StockManagement.Application.Services.Abstract;

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

		// GET: Orders/Create
		public IActionResult Create()
		{
			ViewData["CustomerId"] = new SelectList(_orderService.GetAllCustomersAsync().Result, "Id", "Name");
			ViewData["ProductId"] = new SelectList(_orderService.GetAllProductsAsync().Result, "Id", "Name");
			return View();
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Create(CreateOrderRequest request)
		{
			if (ModelState.IsValid)
			{
				await _orderService.AddAsync(request);
				return RedirectToAction(nameof(Index));
			}
			return View();
		}

		public async Task<IActionResult> Edit(int? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			var order = await _orderService.GetByIdWithCustomerAndProductAsync((int)id);
			if (order == null)
			{
				return NotFound();
			}
			var editRequest = new EditOrderRequest
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
		public async Task<IActionResult> Edit(int id, EditOrderRequest request)
		{
			if (id != request.Id)
			{
				return NotFound();
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
						return NotFound();
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
				return NotFound();
			}

			var order = await _orderService.GetByIdWithCustomerAndProductAsync((int)id);
			if (order == null)
			{
				return NotFound();
			}
			var deleteResponse = new DeleteOrderResponse
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
				return NotFound();
			}

			await _orderService.DeleteAsync((int)id);
			return RedirectToAction(nameof(Index));
		}
	}
}
