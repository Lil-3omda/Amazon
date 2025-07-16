namespace Amazon_API.Models.Entities.Carting.Dtos
{
    public class WishListItemResponseDto
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public string ProductTitle { get; set; }
        public string ProductImage { get; set; }
        public decimal Price { get; set; }
    }
}
