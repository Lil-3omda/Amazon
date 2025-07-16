using Amazon_API.Models.Entities.User;
using Amazon_API.Models.Entities.User.Dtos;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

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

        [HttpPost("Register")]
        public  async Task<IActionResult> Register(RegisterDto registerUserDto)
        {
            var user = mapper.Map<ApplicationUser>(registerUserDto);
            var result = await userManager.CreateAsync(user, registerUserDto.Password);

            if (!result.Succeeded)
                return BadRequest(result.Errors);

            return Ok();
        }

    }
}
