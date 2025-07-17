using Amazon_API.Helpers;
using Amazon_API.Models.Entities.Common;
using Amazon_API.Models.Entities.User;

namespace Amazon_API.Models.Entities.Ordering
{
    public class Order : BaseEntity
    {
        public int Id { get; set; }

        public string UserId { get; set; }
        public ApplicationUser User { get; set; }

        public DateTime OrderDate { get; set; } = DateTime.UtcNow;
        public string Status { get; set; } = OrderStatuses.Pending;

        public decimal TotalPrice { get; set; }

        public string TrackingId { get; set; } = Guid.NewGuid().ToString().Substring(0, 10);

        public ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();

    }
}
