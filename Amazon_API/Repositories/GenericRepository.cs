using Microsoft.EntityFrameworkCore;
using Amazon_API.Data;
using Amazon_API.Interfaces;

namespace Amazon_API.Repositories
{
    public class GenericRepository<T>:IGenericRepository<T> where T : class
    {
        private readonly AppDbContext context;
        private readonly DbSet<T> dbSet;

        public GenericRepository(AppDbContext context)
        {
            this.context = context;
            dbSet = context.Set<T>();
        }

        public async Task<IEnumerable<T>> GetAllAsync() => await dbSet.ToListAsync();
        public async Task<T?> GetByIdAsync(int id) => await dbSet.FindAsync(id);
        public async Task AddAsync(T entity) => await dbSet.AddAsync(entity);
        public void Delete(T entity) => dbSet.Remove(entity);
        public void Update(T entity) => dbSet.Update(entity);
    }
}
