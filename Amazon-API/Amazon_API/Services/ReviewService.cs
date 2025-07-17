using Amazon_API.Data;
using Amazon_API.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Amazon_API.Services
{
    public class ReviewService:IReviewService
    {
        private readonly AppDbContext _context;

        public ReviewService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<bool> SoftDeleteReviewAsync(int productid, int reviewid)
        {
            var review = await _context.Reviews.FirstOrDefaultAsync(r=>r.Id==reviewid && r.ProductId==productid && !r.IsDeleted);
            if (review == null)
                return false;

            review.IsDeleted = true;
            review.DeletedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();
            return true;
        }
    }
}
