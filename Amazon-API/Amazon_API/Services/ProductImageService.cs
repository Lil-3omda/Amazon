using Amazon_API.Data;
using Amazon_API.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Amazon_API.Services
{
    public class ProductImageService : IProductImageService
    {
        private readonly AppDbContext _context;

        public ProductImageService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<bool> SoftDeleteImageAsync(int productid,int imageId)
        {
            var image = await _context.ProductImages.FirstOrDefaultAsync(i => i.Id == imageId && i.ProductId==productid && !i.IsDeleted);
            if (image == null) 
                return false;

            image.IsDeleted = true;
            image.DeletedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();
            return true;
        }
    }
}
