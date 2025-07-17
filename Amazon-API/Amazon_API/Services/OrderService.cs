using Amazon_API.Interfaces;
using Amazon_API.Models.Entities.Ordering;
using Amazon_API.Models.Entities.Ordering.Dtos;
using Amazon_API.Services.Interfaces;
using AutoMapper;

namespace Amazon_API.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public OrderService(IOrderRepository orderRepository, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _orderRepository = orderRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<OrderResponseDto?> GetOrderByIdAsync(int orderId)
        {
            var order = await _orderRepository.GetOrderWithItemsByIdAsync(orderId);
            return order == null ? null : _mapper.Map<OrderResponseDto>(order);
        }

        public async Task<OrderResponseDto?> GetOrderByTrackingIdAsync(string trackingId)
        {
            var order = await _orderRepository.GetOrderWithItemsByTrackingIdAsync(trackingId);
            return order == null ? null : _mapper.Map<OrderResponseDto>(order);
        }

        public async Task<IEnumerable<OrderResponseDto>> GetOrdersByUserIdAsync(string userId)
        {
            var orders = await _orderRepository.GetOrdersByUserIdAsync(userId);
            return _mapper.Map<IEnumerable<OrderResponseDto>>(orders);
        }

        public async Task<IEnumerable<OrderResponseDto>> GetAllOrdersAsync()
        {
            var orders = await _orderRepository.GetAllOrdersWithItemsAsync();
            return _mapper.Map<IEnumerable<OrderResponseDto>>(orders);
        }

        public async Task<bool> UpdateOrderStatusAsync(int orderId, OrderStatusUpdateDto statusDto)
        {
            var order = await _orderRepository.GetByIdAsync(orderId);
            if (order == null || order.IsDeleted)
                return false;

            // Validate status
            if (!Helpers.OrderStatuses.All.Contains(statusDto.Status))
                return false;

            order.Status = statusDto.Status;
            _orderRepository.Update(order);
            await _unitOfWork.SaveAsync();
            return true;
        }

        public async Task<bool> CancelOrderAsync(int orderId, string userId)
        {
            var order = await _orderRepository.GetOrderWithItemsByIdAsync(orderId);
            if (order == null || order.IsDeleted || order.UserId != userId)
                return false;

            if (order.Status == Helpers.OrderStatuses.Cancelled)
                return false;

            order.Status = Helpers.OrderStatuses.Cancelled;
            _orderRepository.Update(order);
            await _unitOfWork.SaveAsync();
            return true;
        }

        public async Task<OrderResponseDto> CreateOrderAsync(string userId, OrderCreateDto orderCreateDto)
        {
            var order = new Order
            {
                UserId = userId,
                OrderDate = DateTime.UtcNow,
                Status = Helpers.OrderStatuses.Pending,
                OrderItems = new List<OrderItem>()
            };

            decimal totalPrice = 0;

            foreach (var itemDto in orderCreateDto.Items)
            {
                var orderItem = new OrderItem
                {
                    ProductId = itemDto.ProductId,
                    Quantity = itemDto.Quantity,
                    // You can fetch Product price from DB here if you want to ensure price correctness
                    UnitPrice = 0m // You should replace this by actual product price lookup
                };
                // TODO: Fetch product price from DB and assign to orderItem.UnitPrice

                order.OrderItems.Add(orderItem);
            }

            // TODO: Calculate totalPrice by summing UnitPrice * Quantity for all orderItems

            order.TotalPrice = totalPrice;

            await _orderRepository.AddAsync(order);
            await _unitOfWork.SaveAsync();

            return _mapper.Map<OrderResponseDto>(order);
        }
    }
}
