using Amazon_API.Models.Entities.Common;

namespace Amazon_API.Models.Entities.Products
{
    public class ProductImage : BaseEntity
    {
        public int Id { get; set; }
        public string ImageUrl { get; set; }

        public int? ProductId { get; set; }
        public Product? Product { get; set; }
    }
}
