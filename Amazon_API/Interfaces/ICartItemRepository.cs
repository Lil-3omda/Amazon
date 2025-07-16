using Amazon_API.Models.Entities.Carting;

namespace Amazon_API.Interfaces
{
    public interface ICartItemRepository : IGenericRepository<CartItem>
    {
        Task<IEnumerable<CartItem>> GetUserCartAsync(string userId);
        Task<CartItem?> GetUserCartItemAsync(string userId, int productId);
    }
}
