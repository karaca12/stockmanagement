namespace StockManagement.Application.DTOs.Responses
{
	public class GetProductByIdWithCategoryResponse
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public string Brand { get; set; }
		public int CategoryId { get; set; }
		public string CategoryName { get; set; }
		public decimal Price { get; set; }
		public int Stock { get; set; }
	}
}
