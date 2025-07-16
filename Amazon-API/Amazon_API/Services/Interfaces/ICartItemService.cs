namespace Amazon_API.Services.Interfaces
{
    public interface ICartItemService
    {
        Task<bool> SoftDeleteCartItemAsync(int cartItemId, string userId);
        Task<bool> ClearCartAsync(string userId);
    }
}
