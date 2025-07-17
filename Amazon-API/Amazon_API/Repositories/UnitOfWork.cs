using Amazon_API.Data;
using Amazon_API.Interfaces;
using Microsoft.VisualBasic;

namespace Amazon_API.Repositories
{
    public class UnitOfWork:IUnitOfWork
    {
        private readonly AppDbContext context;
        private ICartItemRepository? _cartItemRepository;
        private IWishListItemRepository? _wishListItemRepository;
        private IOrderRepository? _orderRepository;

        public UnitOfWork(AppDbContext context)
        {
            this.context = context;
        }
        public ICartItemRepository CartItems =>
            _cartItemRepository ??= new CartItemRepository(context);
        public IWishListItemRepository WishListItems =>
            _wishListItemRepository ??= new WishListItemRepository(context);
        public IOrderRepository Orders =>
            _orderRepository ??= new OrderRepository(context);
        public void Dispose()
        {
            context.Dispose();
        }

        public async Task<int> SaveAsync()
        {
            return await context.SaveChangesAsync();
        }
    }
}
