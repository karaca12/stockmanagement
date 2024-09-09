using System.ComponentModel.DataAnnotations;

namespace StockManagement.Domain.ViewModels.Requests
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
