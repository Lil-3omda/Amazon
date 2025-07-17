namespace Amazon_API.Services.Interfaces
{
    public interface IWishListItemService
    {
        Task<bool> SoftDeleteWishListItemAsync(int wishListItemId, string userId);
        Task<bool> ClearWishListAsync(string userId);
    }
}
