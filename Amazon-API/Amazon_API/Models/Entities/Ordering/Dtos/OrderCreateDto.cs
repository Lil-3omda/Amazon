namespace Amazon_API.Models.Entities.Ordering.Dtos
{
    public class OrderCreateDto
    {
        public List<OrderItemCreateDto> Items { get; set; } = new();

    }
}
