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


        // GET: Categories
        public async Task<IActionResult> Index()
        {
            return View(await _categoryService.GetAllAsync());
        }

        // GET: Categories/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Categories/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
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

        // GET: Categories/Edit/5
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

        // POST: Categories/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
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

        // GET: Categories/Delete/5
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

        // POST: Categories/Delete/5
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
