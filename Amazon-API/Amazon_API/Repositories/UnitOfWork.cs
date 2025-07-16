using Amazon_API.Data;
using Amazon_API.Interfaces;
using Microsoft.VisualBasic;

namespace Amazon_API.Repositories
{
    public class UnitOfWork:IUnitOfWork
    {
        private readonly AppDbContext context;
        private ICartItemRepository? _cartItemRepository;

        public UnitOfWork(AppDbContext context)
        {
            this.context = context;
        }
        public ICartItemRepository CartItems =>
            _cartItemRepository ??= new CartItemRepository(context);
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
