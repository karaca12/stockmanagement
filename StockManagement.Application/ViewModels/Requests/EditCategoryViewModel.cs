using System.ComponentModel.DataAnnotations;

namespace StockManagement.Application.ViewModels.Requests
{
	public class EditCategoryViewModel
	{
		[Required]
		public int Id { get; set; }
		[Required]
		public string Name { get; set; }
	}
}
