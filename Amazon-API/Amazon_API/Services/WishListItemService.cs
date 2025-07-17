using Amazon_API.Data;
using Amazon_API.Services.Interfaces;
using Microsoft.EntityFrameworkCore;


namespace Amazon_API.Services
{
    public class WishListItemService : IWishListItemService
    {
        private readonly AppDbContext _context;

        public WishListItemService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<bool> SoftDeleteWishListItemAsync(int wishListItemId, string userId)
        {
            var wishItem = await _context.WishlistItems
                .FirstOrDefaultAsync(w => w.Id == wishListItemId && w.UserId == userId && !w.IsDeleted);

            if (wishItem == null)
                return false;

            wishItem.IsDeleted = true;
            wishItem.DeletedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> ClearWishListAsync(string userId)
        {
            var wishItems = await _context.WishlistItems
                .Where(w => w.UserId == userId && !w.IsDeleted)
                .ToListAsync();

            if (!wishItems.Any())
                return false;

            foreach (var item in wishItems)
            {
                item.IsDeleted = true;
                item.DeletedAt = DateTime.UtcNow;
            }

            await _context.SaveChangesAsync();
            return true;
        }
    }
}
