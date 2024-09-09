using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using StockManagement.Application.Services.Abstract;
using StockManagement.Domain.ViewModels.Requests;
using StockManagement.Domain.ViewModels.Responses;

namespace StockManagement.Web.Controllers
{
	[Authorize]
	public class ProductsController : Controller
	{
		private readonly IProductService _productService;

		public ProductsController(IProductService productService)
		{
			_productService = productService;
		}

		public async Task<IActionResult> Index(string searchString, int pageNumber = 1, int pageSize = 10)
		{
			var pagedProducts = await _productService.GetAllWithCategoryPagedAsync(pageNumber, pageSize, searchString);
			ViewData["CurrentFilter"] = searchString;
			return View(pagedProducts);
		}

		public IActionResult Create()
		{
			ViewData["CategoryId"] = new SelectList(_productService.GetAllCategoriesAsync().Result, "Id", "Name");
			return View();
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Create(CreateProductViewModel request)
		{
			if (ModelState.IsValid)
			{
				try
				{
					await _productService.AddAsync(request);
					return RedirectToAction(nameof(Index));
				}
				catch (InvalidOperationException ex)
				{
					ModelState.AddModelError(string.Empty, ex.Message);
					ViewData["CategoryId"] = new SelectList(_productService.GetAllCategoriesAsync().Result, "Id", "Name");
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

			var product = await _productService.GetByIdWithCategoryAsync((int)id);
			if (product == null)
			{
				RedirectToAction("PageNotFound", "Error");
			}
			var editRequest = new EditProductViewModel
			{
				Id = product.Id,
				Name = product.Name,
				Brand = product.Brand,
				CategoryId = product.CategoryId,
				Price = product.Price,
				Stock = product.Stock
			};
			ViewData["CategoryId"] = new SelectList(_productService.GetAllCategoriesAsync().Result, "Id", "Name", product.CategoryId);
			return View(editRequest);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Edit(int id, EditProductViewModel request)
		{
			if (id != request.Id)
			{
				return RedirectToAction("PageNotFound", "Error");
			}

			if (ModelState.IsValid)
			{
				try
				{
					await _productService.UpdateAsync(request);
					return RedirectToAction(nameof(Index));
				}
				catch (InvalidOperationException ex)
				{
					ModelState.AddModelError(string.Empty, ex.Message);
					ViewData["CategoryId"] = new SelectList(_productService.GetAllCategoriesAsync().Result, "Id", "Name");
				}
				catch (DbUpdateConcurrencyException)
				{
					if (!await _productService.Exists(request.Id))
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

			var product = await _productService.GetByIdWithCategoryAsync((int)id);
			if (product == null)
			{
				return RedirectToAction("PageNotFound", "Error");
			}
			var deleteResponse = new DeleteProductViewModel
			{
				Id = product.Id,
				Name = product.Name,
				Brand = product.Brand,
				Category = product.CategoryName,
				Price = product.Price,
				Stock = product.Stock
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

			await _productService.DeleteAsync((int)id);
			return RedirectToAction(nameof(Index));
		}
	}
}
