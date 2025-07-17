using Amazon_API.Data;
using Amazon_API.Interfaces;
using Amazon_API.Repositories.ProductRepository;
using Microsoft.VisualBasic;

namespace Amazon_API.Repositories
{
    public class UnitOfWork:IUnitOfWork
    {
        private readonly AppDbContext context;
        public IProductRepository Products { get; }
        public UnitOfWork(AppDbContext context, IProductRepository productRepository)
        {
            this.context = context;
            Products = productRepository;
        }
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
