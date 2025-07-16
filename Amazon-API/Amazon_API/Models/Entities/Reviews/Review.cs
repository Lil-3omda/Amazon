using Amazon_API.Models.Entities.Common;
using Amazon_API.Models.Entities.Products;
using Amazon_API.Models.Entities.User;

namespace Amazon_API.Models.Entities.Reviews
{
    public class Review : BaseEntity
    {
        public int Id { get; set; }

        public int ProductId { get; set; }
        public Product Product { get; set; }

        public string UserId { get; set; }
        public ApplicationUser User { get; set; }

        public int Rating { get; set; } 
        public string Comment { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
