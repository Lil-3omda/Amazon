namespace Amazon_API.Models.Entities.Ordering.Dtos
{
    public class OrderItemDto
    {
        public int ProductId { get; set; }
        public string ProductTitle { get; set; } = string.Empty;
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
    }
}
