using Amazon_API.Data;
using Amazon_API.Services.Interfaces;
using Microsoft.EntityFrameworkCore;


namespace Amazon_API.Services
{
    public class ProductService : IProductService
    {

        private readonly AppDbContext context;

        public ProductService(AppDbContext context)
        {
            this.context = context;
        }

        public async Task<bool> SoftDeleteProductAsync(int productId)
        {
            var product = await context.Products
                .Include(p => p.Reviews)
                .Include(p => p.CartItems)
                .Include(p => p.WishListItems)
                .FirstOrDefaultAsync(p => p.Id == productId);

            if (product == null)
                return false;

            product.IsDeleted = true;
            product.DeletedAt = DateTime.UtcNow;

            foreach (var review in product.Reviews)
                review.IsDeleted = true;

            foreach (var cartItem in product.CartItems)
                cartItem.IsDeleted = true;

            foreach (var wish in product.WishListItems)
                wish.IsDeleted = true;

            await context.SaveChangesAsync();
            return true;
        }
    }
}
