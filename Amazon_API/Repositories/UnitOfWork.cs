using Amazon_API.Data;
using Amazon_API.Interfaces;
using Microsoft.VisualBasic;

namespace Amazon_API.Repositories
{
    public class UnitOfWork:IUnitOfWork
    {
        private readonly AppDbContext context;

        public UnitOfWork(AppDbContext context)
        {
            this.context = context;
        }

        public async Task<int> SaveAsync()
        {
            return await context.SaveChangesAsync();
        }
    }
}
