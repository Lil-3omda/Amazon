using Amazon_API.Data;
using Amazon_API.Interfaces;
using Amazon_API.Models.Entities.Carting;
using Microsoft.EntityFrameworkCore;

namespace Amazon_API.Repositories
{
    public class WishListItemRepository : GenericRepository<WishListItem>, IWishListItemRepository
    {
        private readonly AppDbContext _context;

        public WishListItemRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<WishListItem>> GetUserWishlistAsync(string userId)
        {
            return await _context.WishlistItems
                .Include(w => w.Product)
                .ThenInclude(p => p.Images)
                .Where(w => w.UserId == userId && !w.IsDeleted)
                .ToListAsync();
        }

        public async Task<WishListItem?> GetUserWishlistItemAsync(string userId, int productId)
        {
            return await _context.WishlistItems
                .FirstOrDefaultAsync(w => w.UserId == userId && w.ProductId == productId && !w.IsDeleted);
        }
    }
}
