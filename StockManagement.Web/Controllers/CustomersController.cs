using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StockManagement.Application.DTOs.Requests;
using StockManagement.Application.DTOs.Responses;
using StockManagement.Application.Services.Abstract;

namespace StockManagement.Web.Controllers
{
    public class CustomersController : Controller
    {
        private readonly ICustomerService _customerService;

        public CustomersController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        public async Task<IActionResult> Index(string searchString,int pageNumber=1,int pageSize = 10)
        {
            var pagedCustomers = await _customerService.GetAllPagedAsync( pageNumber, pageSize, searchString);
            ViewData["CurrentFilter"] = searchString;
            return View(pagedCustomers);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateCustomerRequest request)
        {
            if (ModelState.IsValid)
            {
                await _customerService.AddAsync(request);
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

            var customer = await _customerService.GetByIdAsync((int)id);
            if (customer == null)
            {
                return NotFound();
            }
            var editRequest = new EditCustomerRequest
            {
                Id = customer.Id,
                Name = customer.Name,
                Surname = customer.Surname
            };
            return View(editRequest);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, EditCustomerRequest request)
        {
            if (id != request.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _customerService.UpdateAsync(request);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await _customerService.Exists(request.Id))
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

            var customer = await _customerService.GetByIdAsync((int)id);
            if (customer == null)
            {
                return NotFound();
            }
            var deletedResponse = new DeleteCustomerResponse
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
                return NotFound();
            }

            await _customerService.DeleteAsync((int)id);
            return RedirectToAction(nameof(Index));
        }
    }
}
