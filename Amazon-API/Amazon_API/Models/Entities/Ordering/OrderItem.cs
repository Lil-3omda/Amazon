using Amazon_API.Models.Entities.Common;
using Amazon_API.Models.Entities.Products;

namespace Amazon_API.Models.Entities.Ordering
{
    public class OrderItem : BaseEntity
    {
        public int Id { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }

        public int ProductId { get; set; }
        public Product Product { get; set; }

        public int OrderId { get; set; }
        public Order Order { get; set; }
    }
}
