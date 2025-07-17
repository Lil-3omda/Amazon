using Amazon_API.Models.Entities.Ordering;

namespace Amazon_API.Interfaces
{
    public interface IOrderRepository : IGenericRepository<Order>
    {
        Task<Order?> GetOrderWithItemsByIdAsync(int orderId);
        Task<Order?> GetOrderWithItemsByTrackingIdAsync(string trackingId);
        Task<IEnumerable<Order>> GetOrdersByUserIdAsync(string userId);
        Task<IEnumerable<Order>> GetAllOrdersWithItemsAsync();
    }
}
