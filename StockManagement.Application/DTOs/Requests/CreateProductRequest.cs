namespace StockManagement.Application.DTOs.Requests
{
	public class CreateProductRequest
	{
		public string Name { get; set; }
		public string Brand { get; set; }
		public int CategoryId { get; set; }
		public decimal Price { get; set; }
		public int Stock { get; set; }
	}
}
