using Amazon_API.Data;
using Amazon_API.Interfaces;
using Amazon_API.Models.Entities.Carting;
using Microsoft.EntityFrameworkCore;

namespace Amazon_API.Repositories
{
    public class CartItemRepository : GenericRepository<CartItem>,ICartItemRepository
    {
        private readonly AppDbContext _context;

        public CartItemRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<CartItem>> GetUserCartAsync(string userId)
        {
            return await _context.CartItems
                .Include(ci => ci.Product)
                .ThenInclude(p => p.Images)
                .Where(ci => ci.UserId == userId && !ci.IsDeleted)
                .ToListAsync();
        }

        public async Task<CartItem?> GetUserCartItemAsync(string userId, int productId)
        {
            return await _context.CartItems
                .FirstOrDefaultAsync(ci => ci.UserId == userId && ci.ProductId == productId && !ci.IsDeleted);
        }
    }
}
