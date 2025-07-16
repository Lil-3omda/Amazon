using Amazon_API.Interfaces;

namespace Amazon_API.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        ICartItemRepository CartItems { get; }
        Task<int> SaveAsync();
    }
}
