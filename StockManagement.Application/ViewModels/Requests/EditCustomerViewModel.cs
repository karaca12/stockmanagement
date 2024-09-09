using System.ComponentModel.DataAnnotations;

namespace StockManagement.Application.ViewModels.Requests
{
	public class EditCustomerViewModel
	{
		[Required]
		public int Id { get; set; }
		[Required]
		public string Name { get; set; }
		[Required]
		public string Surname { get; set; }
	}
}
