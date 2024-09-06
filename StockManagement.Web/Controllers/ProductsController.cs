using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using StockManagement.Application.DTOs.Requests;
using StockManagement.Application.DTOs.Responses;
using StockManagement.Application.Services.Abstract;

namespace StockManagement.Web.Controllers
{
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
		public async Task<IActionResult> Create(CreateProductRequest request)
		{
			if (ModelState.IsValid)
			{
				await _productService.AddAsync(request);
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

			var product = await _productService.GetByIdWithCategoryAsync((int)id);
			if (product == null)
			{
				return NotFound();
			}
			var editRequest = new EditProductRequest
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
		public async Task<IActionResult> Edit(int id, EditProductRequest request)
		{
			if (id != request.Id)
			{
				return NotFound();
			}

			if (ModelState.IsValid)
			{
				try
				{
					await _productService.UpdateAsync(request);
				}
				catch (DbUpdateConcurrencyException)
				{
					if (!await _productService.Exists(request.Id))
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

			var product = await _productService.GetByIdWithCategoryAsync((int)id);
			if (product == null)
			{
				return NotFound();
			}
			var deleteResponse = new DeleteProductResponse
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
				return NotFound();
			}

			await _productService.DeleteAsync((int)id);
			return RedirectToAction(nameof(Index));
		}
	}
}
