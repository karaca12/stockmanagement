namespace StockManagement.Application.ViewModels.Responses
{
	public class DeleteOrderViewModel
	{
		public int Id { get; set; }
		public string Product { get; set; }
		public string Customer { get; set; }
		public int Pieces { get; set; }
		public decimal Price { get; set; }
	}
}
