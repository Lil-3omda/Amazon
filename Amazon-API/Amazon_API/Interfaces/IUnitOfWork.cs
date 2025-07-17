using Amazon_API.Interfaces;
using Amazon_API.Repositories.ProductRepository;

namespace Amazon_API.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IProductRepository Products { get; }
        ICartItemRepository CartItems { get; }
        IWishListItemRepository WishListItems { get; }
        IOrderRepository Orders { get; }
        Task<int> SaveAsync();
    }
}
