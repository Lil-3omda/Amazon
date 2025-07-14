using Amazon_API.Interfaces;

namespace Amazon_API.Interfaces
{
    public interface IUnitOfWork
    {
        Task<int> SaveAsync();
    }
}
