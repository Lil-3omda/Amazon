using Amazon_API.Data;
using Amazon_API.Interfaces;
using Amazon_API.Repositories.ProductRepository;
using Amazon_API.Repositories.CartRepository;
using Amazon_API.Repositories.WishlistRepository;
using Amazon_API.Repositories.OrderRepository;

namespace Amazon_API.Repositories
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly AppDbContext context;

        private ICartItemRepository? _cartItemRepository;
        private IWishListItemRepository? _wishListItemRepository;
        private IOrderRepository? _orderRepository;

        public IProductRepository Products { get; }

        public UnitOfWork(AppDbContext context, IProductRepository productRepository)
        {
            this.context = context;
            Products = productRepository;
        }

        public ICartItemRepository CartItems =>
            _cartItemRepository ??= new CartItemRepository(context);

        public IWishListItemRepository WishListItems =>
            _wishListItemRepository ??= new WishListItemRepository(context);

        public IOrderRepository Orders =>
            _orderRepository ??= new OrderRepository(context);

        public async Task<int> SaveAsync()
        {
            return await context.SaveChangesAsync();
        }

        public void Dispose()
        {
            context.Dispose();
        }
    }
}
