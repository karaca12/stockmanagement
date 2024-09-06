namespace StockManagement.Application.DTOs.Responses
{
    public class GetOrderByIdWithCustomerAndProductResponse
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public int CustomerId { get; set; }
        public string CustomerName { get; set; }
        public int Pieces { get; set; }
        public decimal Price { get; set; }
    }
}
