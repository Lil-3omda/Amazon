using Amazon_API.Models.Entities.Ordering;
using Amazon_API.Models.Entities.Ordering.Dtos;
using Amazon_API.Services.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Amazon_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;
        private readonly IMapper _mapper;

        public OrderController(IOrderService orderService, IMapper mapper)
        {
            _orderService = orderService;
            _mapper = mapper;
        }
        private string GetUserId()
        {
            return User.FindFirstValue(ClaimTypes.NameIdentifier)!;
        }

        // GET: api/order/{id}
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetOrderById(int id)
        {
            var order = await _orderService.GetOrderByIdAsync(id);
            if (order == null)
                return NotFound(new { message = "Order not found" });
            
            return Ok(order);
        }

        // GET: api/order/track/{trackingId}
        [HttpGet("track/{trackingId}")]
        public async Task<IActionResult> GetOrderByTrackingId(string trackingId)
        {
            var order = await _orderService.GetOrderByTrackingIdAsync(trackingId);
            if (order == null)
                return NotFound(new { message = "Order not found" });

            return Ok(order);
        }

        [HttpGet("my")]
        public async Task<IActionResult> GetMyOrders()
        {
            var userId = GetUserId();
            var orders = await _orderService.GetOrdersByUserIdAsync(userId);

            return Ok(orders);
        }

        // GET: api/order/all
        [HttpGet("all")]
        //[Authorize(Roles = "")] 
        public async Task<IActionResult> GetAllOrders()
        {
            var orders = await _orderService.GetAllOrdersAsync();
            
            return Ok(orders);
        }

        // PUT: api/order/{id}/status
        [HttpPut("{id:int}/status")]
        //[Authorize(Roles = "")]
        public async Task<IActionResult> UpdateOrderStatus(int id, [FromBody] OrderStatusUpdateDto statusDto)
        {
            var success = await _orderService.UpdateOrderStatusAsync(id, statusDto);
            
            if (!success)
                return BadRequest(new { message = "Invalid order or status" });
            
            return Ok(new { message = "Order status updated" });
        }

        // POST: api/order/{id}/cancel
        [HttpPost("{id:int}/cancel")]
        public async Task<IActionResult> CancelOrder(int id)
        {
            var userId = GetUserId();
            var success = await _orderService.CancelOrderAsync(id, userId);
            
            if (!success)
                return BadRequest(new { message = "Cannot cancel this order" });
            
            return Ok(new { message = "Order cancelled" });
        }

        // POST: api/order
        [HttpPost]
        public async Task<IActionResult> CreateOrder([FromBody] OrderCreateDto orderCreateDto)
        {
            var userId = GetUserId();
            var order = await _orderService.CreateOrderAsync(userId, orderCreateDto);
            
            return CreatedAtAction(nameof(GetOrderById), new { id = order.Id }, order);
        }
    }
}
