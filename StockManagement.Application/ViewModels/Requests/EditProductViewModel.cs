using System.ComponentModel.DataAnnotations;

namespace StockManagement.Application.ViewModels.Requests
{
	public class EditProductViewModel
	{
		[Required]
		public int Id { get; set; }
		[Required]
		public string Name { get; set; }
		[Required]
		public string Brand { get; set; }
		[Required]
		public int CategoryId { get; set; }
		[Required]
		public decimal Price { get; set; }
		[Required]
		public int Stock { get; set; }
	}
}
