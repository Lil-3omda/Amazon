using Amazon_API.Data;
using Amazon_API.Models.Entities.Products;
using Microsoft.EntityFrameworkCore;

namespace Amazon_API.Repositories.ProductRepository
{
    public class ProductRepository : GenericRepository<Product>, IProductRepository
    {
        private readonly AppDbContext _context;

        public ProductRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Product>> GetProductsByCategoryIdAsync(int categoryId)
        {
            return await _context.Products.Where(p => p.CategoryId == categoryId).Include(p => p.Images).Include(p => p.Category).ToListAsync();
              
        }

        public async Task<IEnumerable<Product>> SearchProductsByTitleAsync(string title)
        {
            return await _context.Products
                .Where(p => p.Title.Contains(title))
                .Include(p => p.Images)
                .ToListAsync();
        }

        public async Task<IEnumerable<Product>> GetProductsBySellerIdAsync(string sellerId)
        {
            return await _context.Products
                .Where(p => p.SellerId == sellerId)
                .Include(p => p.Images)
                .ToListAsync();
        }

        public async Task<IEnumerable<Product>> GetTopSellingProductsAsync(int count)
        {
            var topProductIds = await _context.OrderItems
                .GroupBy(oi => oi.ProductId)
                .OrderByDescending(g => g.Sum(oi => oi.Quantity))
                .Take(count)
                .Select(g => g.Key)
                .ToListAsync();

            return await _context.Products
                .Where(p => topProductIds.Contains(p.Id))
                .Include(p => p.Images)
                .ToListAsync();
        }

        public async Task<IEnumerable<Product>> GetRelatedProductsAsync(int productId)
        {
            var currentProduct = await _context.Products.FindAsync(productId);
            if (currentProduct == null) return new List<Product>();

            return await _context.Products
                .Where(p => p.CategoryId == currentProduct.CategoryId && p.Id != productId)
                .Include(p => p.Images)
                .ToListAsync();
        }
        public async Task<IEnumerable<Product>> FilterProductsAsync(decimal? minPrice, decimal? maxPrice, string sortBy)
        {
            var query = _context.Products
                .Include(p => p.Images)
                .AsQueryable();

            // Apply price filter
            if (minPrice.HasValue)
                query = query.Where(p => p.Price >= minPrice.Value);

            if (maxPrice.HasValue)
                query = query.Where(p => p.Price <= maxPrice.Value);

            // Apply sorting
            query = sortBy switch
            {
                "price_asc" => query.OrderBy(p => p.Price),
                "price_desc" => query.OrderByDescending(p => p.Price),
                "name_asc" => query.OrderBy(p => p.Title),
                "name_desc" => query.OrderByDescending(p => p.Title),
                _ => query.OrderBy(p => p.Id) // default sort
            };

            return await query.ToListAsync();
        }


        public async Task<IEnumerable<Product>> GetPagedProductsAsync(int pageNumber, int pageSize)
        {
            return await _context.Products
                .Include(p => p.Images)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

       
    }
}
