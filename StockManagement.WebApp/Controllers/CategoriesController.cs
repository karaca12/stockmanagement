using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StockManagement.Application.Services.Abstract;
using StockManagement.Domain.ViewModels.Requests;
using StockManagement.Domain.ViewModels.Responses;

namespace StockManagement.Web.Controllers;

[Authorize]
public class CategoriesController : Controller
{
    private readonly ICategoryService _categoryService;

    public CategoriesController(ICategoryService categoryService)
    {
        _categoryService = categoryService;
    }


    public async Task<IActionResult> Index(string searchString, int pageNumber = 1, int pageSize = 10)
    {
        var pagedCategories = await _categoryService.GetAllPagedAsync(pageNumber, pageSize, searchString);
        ViewData["CurrentFilter"] = searchString;
        return View(pagedCategories);
    }


    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(CreateCategoryViewModel request)
    {
        if (ModelState.IsValid)
            try
            {
                await _categoryService.AddAsync(request);
                return RedirectToAction(nameof(Index));
            }
            catch (InvalidOperationException ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
            }

        return View();
    }

    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null) return RedirectToAction("PageNotFound", "Error");

        var category = await _categoryService.GetByIdAsync((int)id);
        if (category == null) return RedirectToAction("PageNotFound", "Error");
        var editRequest = new EditCategoryViewModel
        {
            Id = category.Id,
            Name = category.Name
        };
        return View(editRequest);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, EditCategoryViewModel request)
    {
        if (id != request.Id) return RedirectToAction("PageNotFound", "Error");

        if (ModelState.IsValid)
            try
            {
                await _categoryService.UpdateAsync(request);
                return RedirectToAction(nameof(Index));
            }
            catch (InvalidOperationException ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await _categoryService.Exists(request.Id))
                    return RedirectToAction("PageNotFound", "Error");
                throw;
            }

        return View();
    }

    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null) return RedirectToAction("PageNotFound", "Error");

        var category = await _categoryService.GetByIdAsync((int)id);
        if (category == null) return RedirectToAction("PageNotFound", "Error");
        var deleteResponse = new DeleteCategoryViewModel
        {
            Id = category.Id,
            Name = category.Name
        };

        return View(deleteResponse);
    }

    [HttpPost]
    [ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int? id)
    {
        if (id == null) return RedirectToAction("PageNotFound", "Error");

        await _categoryService.DeleteAsync((int)id);


        return RedirectToAction(nameof(Index));
    }
}