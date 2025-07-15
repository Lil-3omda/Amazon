namespace Amazon_API.Models.Entities.Products.Dtos
{
    public class ProductResponseDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }

        public int CategoryId { get; set; }
        public string CategoryName { get; set; }

        public string SellerId { get; set; }
        public string SellerName { get; set; }

        public List<string> ImageUrls { get; set; }

    }
}
