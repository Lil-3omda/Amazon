using Amazon_API.Models.Entities.Ordering.Dtos;

namespace Amazon_API.Services.Interfaces
{
    public interface IOrderService
    {
        Task<OrderResponseDto?> GetOrderByIdAsync(int orderId);
        Task<OrderResponseDto?> GetOrderByTrackingIdAsync(string trackingId);
        Task<IEnumerable<OrderResponseDto>> GetOrdersByUserIdAsync(string userId);
        Task<IEnumerable<OrderResponseDto>> GetAllOrdersAsync();
        Task<bool> UpdateOrderStatusAsync(int orderId, OrderStatusUpdateDto statusDto);
        Task<bool> CancelOrderAsync(int orderId, string userId);
        Task<OrderResponseDto> CreateOrderAsync(string userId, OrderCreateDto orderCreateDto);
    }
}
