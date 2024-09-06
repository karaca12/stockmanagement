namespace StockManagement.Application.DTOs.Requests
{
	public class CreateOrderRequest
	{
		public int ProductId { get; set; }
		public int CustomerId { get; set; }
		public int Pieces { get; set; }
		public decimal Price { get; set; }
	}
}
