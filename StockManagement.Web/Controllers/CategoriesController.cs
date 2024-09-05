using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StockManagement.Application.DTOs.Requests;
using StockManagement.Application.DTOs.Responses;
using StockManagement.Application.Services.Abstract;

namespace StockManagement.Web.Controllers
{
    public class CategoriesController : Controller
    {
        private readonly ICategoryService _categoryService;

        public CategoriesController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }


        public async Task<IActionResult> Index()
        {
            return View(await _categoryService.GetAllAsync());
        }


        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,Id,CreatedAt,UpdatedAt,IsDeleted,DeletedAt")] CreateCategoryRequest request)
        {
            if (ModelState.IsValid)
            {
                await _categoryService.AddAsync(request);
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

            var category = await _categoryService.GetByIdAsync((int)id);
            if (category == null)
            {
                return NotFound();
            }
            var editRequest = new EditCategoryRequest
            {
                Id = category.Id,
                Name = category.Name
            };
            return View(editRequest);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Name,Id,CreatedAt,UpdatedAt,IsDeleted,DeletedAt")] EditCategoryRequest request)
        {
            if (id != request.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _categoryService.UpdateAsync(request);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await _categoryService.Exists(request.Id))
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

            var category = await _categoryService.GetByIdAsync((int)id);
            if (category == null)
            {
                return NotFound();
            }
            var deleteResponse = new DeleteCategoryResponse
            {
                Id = category.Id,
                Name = category.Name
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

            await _categoryService.DeleteAsync((int)id);


            return RedirectToAction(nameof(Index));
        }
    }
}
