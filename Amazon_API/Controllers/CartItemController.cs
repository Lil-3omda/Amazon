using Amazon_API.Interfaces;
using Amazon_API.Models.Entities.Carting;
using Amazon_API.Models.Entities.Carting.Dtos;
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
    //[Authorize(Roles ="Customer")]
    public class CartItemController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICartItemService _cartItemService;
        private readonly IMapper _mapper;

        public CartItemController(IUnitOfWork unitOfWork,ICartItemService cartItemService,IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _cartItemService = cartItemService;
            _mapper = mapper;
        }

        private string GetUserId()
        {
            return User.FindFirstValue(ClaimTypes.NameIdentifier)!;
        }

        // GET: api/CartItem
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CartItemResponseDto>>> GetCartItems()
        {
            var userId = GetUserId();
            var cartItems = await _unitOfWork.CartItems.GetUserCartAsync(userId);
            var result = _mapper.Map<List<CartItemResponseDto>>(cartItems);
            return Ok(result);
        }

        // POST: api/CartItem
        [HttpPost]
        public async Task<ActionResult> AddToCart([FromBody] CartItemCreateDto dto)
        {
            var userId = GetUserId();
            var existingItem = await _unitOfWork.CartItems.GetUserCartItemAsync(userId,dto.ProductId);

            if (existingItem !=null)
            {
                existingItem.Quantity += dto.Quantity;
                _unitOfWork.CartItems.Update(existingItem);
            }
            else
            {
                var cartItem = _mapper.Map<CartItem>(dto);
                cartItem.UserId = userId;
                await _unitOfWork.CartItems.AddAsync(cartItem);
            }

            await _unitOfWork.SaveAsync();
            return Ok(new { message = "Item added to cart successfully" });
        }

        // PUT: api/CartItem
        [HttpPut]
        public async Task<ActionResult> UpdateQuantity([FromBody] CartItemUpdateDto dto)
        {
            var userId = GetUserId();
            var item = await _unitOfWork.CartItems.GetByIdAsync(dto.CartItemId);

            if (item == null || item.UserId != userId)
                return NotFound();

            item.Quantity=dto.Quantity;
            _unitOfWork.CartItems.Update(item);
            await _unitOfWork.SaveAsync();

            return Ok(new { message = "Quantity updated" });
        }

        // DELETE: api/CartItem/{cartItemId}
        [HttpDelete("{cartItemId}")]
        public async Task<ActionResult> RemoveCartItem(int cartItemId)
        {
            var userId = GetUserId();
            var success = await _cartItemService.SoftDeleteCartItemAsync(cartItemId,userId);

            if(!success)
                return NotFound();

            return Ok(new { message = "Item removed from cart" });
        }

        // DELETE: api/CartItem/clear
        [HttpDelete("clear")]
        public async Task<ActionResult> ClearCart()
        {
            var userId = GetUserId();
            var success = await _cartItemService.ClearCartAsync(userId);

            if(!success)
                return NotFound(new { message = "Cart already empty" });

            return Ok(new { message = "Cart cleared" });
        }

        // POST: api/CartItem/checkout
        [HttpPost("checkout")]
        public async Task<ActionResult> Checkout()
        {
            var userId = GetUserId();
            var cartItems = await _unitOfWork.CartItems.GetUserCartAsync(userId);

            if (!cartItems.Any())
                return BadRequest(new { message = "Cart is empty" });

            foreach (var item in cartItems)
            {
                item.Product.Quantity -= item.Quantity;
                item.IsDeleted = true;
                item.DeletedAt=DateTime.UtcNow;
            }

            await _unitOfWork.SaveAsync();

            return Ok(new { message = "Checkout completed successfully" });
        }
    }
}
