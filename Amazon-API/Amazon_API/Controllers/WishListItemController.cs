using Amazon_API.Interfaces;
using Amazon_API.Models.Entities.Carting;
using Amazon_API.Models.Entities.Carting.Dtos;
using Amazon_API.Services.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Amazon_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize(Roles ="Customer")]
    public class WishListItemController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWishListItemService _wishListItemService;
        private readonly IMapper _mapper;

        public WishListItemController(IUnitOfWork unitOfWork, IWishListItemService wishListItemService, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _wishListItemService = wishListItemService;
            _mapper = mapper;
        }
        private string GetUserId()
        {
            return User.FindFirstValue(ClaimTypes.NameIdentifier)!;
        }

        // GET: api/wishlist
        [HttpGet("wishlist")]
        public async Task<ActionResult<IEnumerable<WishListItemResponseDto>>> GetWishlist()
        {
            var userId = GetUserId();
            var items = await _unitOfWork.WishListItems.GetUserWishlistAsync(userId);
            var result = _mapper.Map<List<WishListItemResponseDto>>(items);
            return Ok(result);
        }

        // POST: api/wishlist/add
        [HttpPost("wishlist/add")]
        public async Task<ActionResult> AddToWishlist([FromBody] WishListItemCreateDto dto)
        {
            var userId = GetUserId();
            var existingItem = await _unitOfWork.WishListItems.GetUserWishlistItemAsync(userId, dto.ProductId);

            if (existingItem != null) 
                return BadRequest(new { message = "Product is already in wishlist" });
            
            var item = _mapper.Map<WishListItem>(dto);
            item.UserId = userId;
            await _unitOfWork.WishListItems.AddAsync(item);
            await _unitOfWork.SaveAsync();

            return Ok(new { message = "Item added to wishlist successfully" });
        }

        // DELETE: api/wishlist/{wishListItemId}
        [HttpDelete("wishlist/{wishListItemId}")]
        public async Task<ActionResult> RemoveWishlistItem(int wishListItemId)
        {
            var userId = GetUserId();
            var success = await _wishListItemService.SoftDeleteWishListItemAsync(wishListItemId, userId);
            if (!success)
                return NotFound();

            return Ok(new { message = "Item removed from wishlist" });
        }

        // DELETE: api/wishlist/clear
        [HttpDelete("wishlist/clear")]
        public async Task<ActionResult> ClearWishlist()
        {
            var userId = GetUserId();
            var success = await _wishListItemService.ClearWishListAsync(userId);

            if (!success)
                return NotFound(new { message = "Wishlist is already empty" });

            return Ok(new { message = "Wishlist cleared" });
        }

        // GET: api/wishlist/checkitem/{productId}
        [HttpGet("wishlist/checkitem/{productId}")]
        public async Task<ActionResult<bool>> CheckIfInWishlist(int productId)
        {
            var userId = GetUserId();
            var item = await _unitOfWork.WishListItems.GetUserWishlistItemAsync(userId, productId);
            return Ok(item != null);
        }



    }
}
