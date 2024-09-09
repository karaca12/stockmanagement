using System.ComponentModel.DataAnnotations;

namespace StockManagement.Application.ViewModels.Requests
{
	public class CreateCategoryViewModel
	{
		[Required]
		public string Name { get; set; }
	}
}
