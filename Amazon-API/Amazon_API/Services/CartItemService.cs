using Amazon_API.Data;
using Amazon_API.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Amazon_API.Services
{
    public class CartItemService : ICartItemService
    {
        private readonly AppDbContext _context;

        public CartItemService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<bool> SoftDeleteCartItemAsync(int cartItemId, string userId)
        {
            var cartItem = await _context.CartItems
                .FirstOrDefaultAsync(ci => ci.Id == cartItemId && ci.UserId == userId && !ci.IsDeleted);

            if (cartItem == null)
                return false;

            cartItem.IsDeleted = true;
            cartItem.DeletedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> ClearCartAsync(string userId)
        {
            var cartItems = await _context.CartItems
                .Where(ci => ci.UserId == userId && !ci.IsDeleted)
                .ToListAsync();

            if (!cartItems.Any())
                return false;

            foreach (var item in cartItems)
            {
                item.IsDeleted = true;
                item.DeletedAt = DateTime.UtcNow;
            }

            await _context.SaveChangesAsync();
            return true;
        }
    }
}
