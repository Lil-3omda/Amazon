namespace Amazon_API.Models.Entities.Ordering.Dtos
{
    public class OrderResponseDto
    {
        public int OrderId { get; set; }
        public DateTime OrderDate { get; set; }
        public string Status { get; set; }

        public decimal TotalPrice { get; set; }

        public List<OrderItemResponseDto> Items { get; set; }
    }

    public class OrderItemResponseDto
    {
        public int ProductId { get; set; }
        public string ProductTitle { get; set; }
        public decimal UnitPrice { get; set; }
        public int Quantity { get; set; }
    }
}
