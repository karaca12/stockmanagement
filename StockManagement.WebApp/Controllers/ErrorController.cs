using Microsoft.AspNetCore.Mvc;

namespace StockManagement.Web.Controllers
{
	public class ErrorController : Controller
	{
		public IActionResult PageNotFound(string errorMessage)
		{
			ViewBag.ErrorMessage = errorMessage ?? "The page you are looking for could not be found.";
			return View();
		}
	}
}
