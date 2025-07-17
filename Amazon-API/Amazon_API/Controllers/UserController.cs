using Amazon_API.Models.Entities.User;
using Amazon_API.Models.Entities.User.Dtos;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace Amazon_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IMapper mapper;

        public UserController(UserManager<ApplicationUser> userManager, IMapper mapper)
        {
            this.userManager = userManager;
            this.mapper = mapper;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserProfile(string id)
        {
            var user = await userManager.FindByIdAsync(id);
            if (user == null)
                return NotFound("User not found");

            var userDto = mapper.Map<UserProfileDto>(user);
            return Ok(userDto);
        }

        [HttpGet("me")]
        public async Task<IActionResult> GetMyProfile()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = await userManager.FindByIdAsync(userId);

            if (user == null) return NotFound();

            var profileDto = mapper.Map<UserProfileDto>(user);
            return Ok(profileDto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(string id, [FromBody] UpdateUserDto updateDto)
        {
            var user = await userManager.FindByIdAsync(id);
            if (user == null)
                return NotFound("User not found");

            user.FullName = updateDto.FullName ?? user.FullName;

            if (updateDto.Address != null)
            {
                user.Address = updateDto.Address.Address ?? user.Address;
                user.City = updateDto.Address.City ?? user.City;
                user.Country = updateDto.Address.Country ?? user.Country;
                user.PostelCode = updateDto.Address.PostelCode ?? user.PostelCode;
            }

            user.ProfileImageUrl = updateDto.ProfileImageUrl ?? user.ProfileImageUrl;

            var result = await userManager.UpdateAsync(user);
            if (!result.Succeeded)
                return BadRequest(result.Errors);

            return Ok("User updated successfully.");
        }

        [HttpPut("update")]
        public async Task<IActionResult> UpdateMyProfile(UpdateUserDto dto)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = await userManager.FindByIdAsync(userId);
            if (user == null) return NotFound();

            user.FullName = dto.FullName;
            user.Address = dto.Address?.Address;
            user.City = dto.Address?.City;
            user.Country = dto.Address?.Country;
            user.PostelCode = dto.Address?.PostelCode;
            user.ProfileImageUrl = dto.ProfileImageUrl;

            var result = await userManager.UpdateAsync(user);
            if (!result.Succeeded)
                return BadRequest(result.Errors);

            return Ok("Profile updated successfully.");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(string id)
        {
            var user = await userManager.FindByIdAsync(id);
            if (user == null)
                return NotFound("User not found");

            var result = await userManager.DeleteAsync(user);
            if (!result.Succeeded)
                return BadRequest(result.Errors);

            return Ok("User deleted.");
        }

        [HttpGet]
        public async Task<IActionResult> GetAllUsers([FromQuery] string? search, [FromQuery] string? country, [FromQuery] int page = 1,[FromQuery] int pageSize = 10)
        {
            var query = userManager.Users.AsQueryable();

            if (!string.IsNullOrWhiteSpace(search))
            {
                search = search.ToLower();
                query = query.Where(u =>
                    u.FullName.ToLower().Contains(search) ||
                    u.Email.ToLower().Contains(search));
            }

            if (!string.IsNullOrWhiteSpace(country))
            {
                query = query.Where(u => u.Country.ToLower() == country.ToLower());
            }

            var totalCount = await query.CountAsync();

            var users = await query
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            var userDtos = mapper.Map<List<UserProfileDto>>(users);

            return Ok(new
            {
                TotalCount = totalCount,
                Page = page,
                PageSize = pageSize,
                Users = userDtos
            });
        }

    }
}
