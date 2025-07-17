using Amazon_API.Models.Entities.Carting;

namespace Amazon_API.Interfaces
{
    public interface IWishListItemRepository : IGenericRepository<WishListItem>
    {
        Task<IEnumerable<WishListItem>> GetUserWishlistAsync(string userId);
        Task<WishListItem?> GetUserWishlistItemAsync(string userId, int productId);
    }
}
