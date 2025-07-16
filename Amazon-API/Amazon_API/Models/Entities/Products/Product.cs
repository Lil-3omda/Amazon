using Amazon_API.Models.Entities.User;
using Amazon_API.Models.Entities.Categories;
using Amazon_API.Models.Entities.Reviews;
using Amazon_API.Models.Entities.Common;
using Amazon_API.Models.Entities.Carting;
namespace Amazon_API.Models.Entities.Products
{
    public class Product : BaseEntity
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public int Quantity { get; set; }

        public int CategoryId { get; set; }
        public Category Category { get; set; }

        public string SellerId { get; set; }
        public ApplicationUser Seller { get; set; }

        public ICollection<ProductImage> Images { get; set; } = new List<ProductImage>();
        public ICollection<Review> Reviews { get; set; } = new List<Review>();
        public ICollection<CartItem> CartItems { get; set; } = new List<CartItem>();
        public ICollection<WishListItem> WishListItems { get; set; } = new List<WishListItem>();


    }
}
