namespace StockManagement.Application.DTOs.Responses
{
	public class GetAllProductsResponse
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public string Brand { get; set; }
		public decimal Price { get; set; }
		public int Stock { get; set; }
	}
}
