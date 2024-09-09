using System.ComponentModel.DataAnnotations;

namespace StockManagement.Application.ViewModels.Requests
{
	public class EditOrderViewModel
	{
		[Required]
		public int Id { get; set; }
		[Required]
		public int ProductId { get; set; }
		[Required]
		public int CustomerId { get; set; }
		[Required]
		public int Pieces { get; set; }
		[Required]
		public decimal Price { get; set; }
	}
}
