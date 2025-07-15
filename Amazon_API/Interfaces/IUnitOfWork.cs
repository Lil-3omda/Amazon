using Amazon_API.Interfaces;

namespace Amazon_API.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        Task<int> SaveAsync();
    }
}
