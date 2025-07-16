namespace Amazon_API.Models.Entities.Products.Dtos
{
    public class ProductCreateDto
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public int CategoryId { get; set; }
        public List<string> ImageUrls { get; set; }
    }
}
