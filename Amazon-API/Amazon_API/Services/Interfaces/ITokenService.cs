using Amazon_API.Models.Entities.User;

namespace Amazon_API.Services.Interfaces
{
    public interface ITokenService
    {
        string CreateToken(ApplicationUser user);
    }
}
