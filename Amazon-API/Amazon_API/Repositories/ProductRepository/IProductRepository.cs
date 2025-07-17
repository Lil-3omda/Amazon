using Amazon_API.Interfaces;
using Amazon_API.Models.Entities.Products;

namespace Amazon_API.Repositories.ProductRepository
{
    public interface IProductRepository:IGenericRepository<Product> 
    {
        Task<IEnumerable<Product>> GetProductsByCategoryIdAsync(int categoryId);
        Task<IEnumerable<Product>> SearchProductsByTitleAsync(string title);
        Task<IEnumerable<Product>> GetProductsBySellerIdAsync(string sellerId);
        Task<IEnumerable<Product>> GetTopSellingProductsAsync(int count); 
        Task<IEnumerable<Product>> GetRelatedProductsAsync(int productId); 
        Task<IEnumerable<Product>> FilterProductsAsync(decimal? minPrice, decimal? maxPrice, string sortBy);

        Task<IEnumerable<Product>> GetPagedProductsAsync(int pageNumber, int pageSize);

    }
}
