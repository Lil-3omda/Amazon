namespace Amazon_API.Models.Entities.Ordering.Dtos
{
    public class OrderResponseDto
    {
        public int Id { get; set; }
        public string TrackingId { get; set; } = string.Empty;
        public DateTime OrderDate { get; set; }
        public string Status { get; set; } = string.Empty;
        public decimal TotalPrice { get; set; }
        public List<OrderItemDto> OrderItems { get; set; } = new();
    }
}
