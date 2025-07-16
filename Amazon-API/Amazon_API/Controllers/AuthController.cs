using Amazon_API.Models.Entities.User;
using Amazon_API.Models.Entities.User.Dtos;
using Amazon_API.Services;
using Amazon_API.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Amazon_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly ITokenService tokenService;
        private readonly IEmailService emailService;

        public AuthController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, ITokenService tokenService, IEmailService emailService)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.tokenService = tokenService;
            this.emailService = emailService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterDto dto)
        {
            var existingUser = await userManager.FindByEmailAsync(dto.Email);
            if (existingUser != null)
                return BadRequest("Email already exists.");

            var user = new ApplicationUser
            {
                FullName = dto.FullName,
                Email = dto.Email,
                UserName = dto.Email,
                IsActive = false 
            };

            var result = await userManager.CreateAsync(user, dto.Password);
            if (!result.Succeeded)
                return BadRequest(result.Errors);

            var otp = new Random().Next(100000, 999999).ToString();

            user.OtpCode = otp;
            user.OtpGeneratedAt = DateTime.UtcNow;
            await userManager.UpdateAsync(user);

            await emailService.SendEmailAsync(user.Email, "Your OTP Code", $"Your code is: {otp}");


            return Ok("Registered successfully. Please check your email for the OTP.");
        }

        [HttpPost("verify-otp")]
        public async Task<IActionResult> VerifyOtp(OtpVerificationDto dto)
        {
            var user = await userManager.FindByEmailAsync(dto.Email);
            if (user == null) return NotFound("User not found.");

            if (user.OtpCode != dto.OtpCode || user.OtpGeneratedAt == null)
                return BadRequest("Invalid or expired OTP.");

            if ((DateTime.UtcNow - user.OtpGeneratedAt.Value).TotalMinutes > 10)
                return BadRequest("OTP expired.");

            user.IsActive = true;
            user.OtpCode = null;
            user.OtpGeneratedAt = null;
            await userManager.UpdateAsync(user);

            var token = tokenService.CreateToken(user);

            return Ok(new
            {
                message = "Email verified successfully.",
                token
            });
        }


        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto dto)
        {
            var user = await userManager.FindByEmailAsync(dto.Email);
            if (user == null)
                return Unauthorized("Invalid email or password.");

            var result = await signInManager.CheckPasswordSignInAsync(user, dto.Password, false);
            if (!result.Succeeded)
                return Unauthorized("Invalid email or password.");

            var token = tokenService.CreateToken(user);

            return Ok(new
            {
                token,
                expires = DateTime.UtcNow.AddHours(240),
                user = new
                {
                    user.Id,
                    user.FullName,
                    user.Email,
                    user.ProfileImageUrl
                }
            });
        }
    }
}
