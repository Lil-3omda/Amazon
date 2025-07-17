using Amazon_API.Models.Entities.Seller;
using Amazon_API.Models.Entities.User.Dtos.SellerDtos;
using Amazon_API.Models.Entities.User;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Amazon_API.Repositories;
using Amazon_API.Data;
using Org.BouncyCastle.Tls;

namespace Amazon_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SellerOnboardingController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IMapper mapper;
        private readonly AppDbContext context;

        public SellerOnboardingController(UserManager<ApplicationUser> userManager, IMapper mapper, AppDbContext context)
        {
            this.userManager = userManager;
            this.mapper = mapper;
            this.context = context;
        }

        [HttpPost("step1")]
        public async Task<IActionResult> SaveBusinessInfo([FromBody] SellerBusinessInfoDto dto)
        {
            var user = await userManager.GetUserAsync(User);
            var sellerProfile = user.SellerProfile ?? new SellerProfile();

            mapper.Map(dto, sellerProfile);
            user.SellerProfile = sellerProfile;

            await context.SaveChangesAsync();
            return Ok("Business info saved.");
        }

        [HttpPost("step2")]
        public async Task<IActionResult> SaveIdentityInfo([FromBody] SellerIdentityInfoDto dto)
        {
            var user = await userManager.GetUserAsync(User);
            mapper.Map(dto, user.SellerProfile);

            await context.SaveChangesAsync();
            return Ok("Identity info saved.");
        }

        [HttpPost("step3")]
        public async Task<IActionResult> SaveBillingInfo([FromBody] SellerBillingInfoDto dto)
        {
            var user = await userManager.GetUserAsync(User);
            mapper.Map(dto, user.SellerProfile);

            await context.SaveChangesAsync();
            return Ok("Billing info saved.");
        }

        [HttpPost("step4")]
        public async Task<IActionResult> SaveStoreInfo([FromBody] SellerStoreInfoDto dto)
        {
            var user = await userManager.GetUserAsync(User);
            mapper.Map(dto, user.SellerProfile);

            await context.SaveChangesAsync();
            return Ok("Store info saved.");
        }

        [HttpPost("step5")]
        public async Task<IActionResult> SaveVerificationInfo([FromBody] SellerVerificationInfoDto dto)
        {
            var user = await userManager.GetUserAsync(User);
            mapper.Map(dto, user.SellerProfile);

            await context.SaveChangesAsync();
            return Ok("Verification info saved.");
        }
    }
}
