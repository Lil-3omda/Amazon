using Amazon_API.Models.Entities.Common;
using Amazon_API.Models.Entities.Products;
using Amazon_API.Models.Entities.User;

namespace Amazon_API.Models.Entities.Carting
{
    public class CartItem : BaseEntity
    {
        public int Id { get; set; }

        public string UserId { get; set; }
        public ApplicationUser User { get; set; }

        public int ProductId { get; set; }
        public Product Product { get; set; }

        public int Quantity { get; set; }
    }
}
