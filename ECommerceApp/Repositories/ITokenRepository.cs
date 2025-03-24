using Microsoft.AspNetCore.Identity;

namespace ECommerceApp.Repositories
{
    public interface ITokenRepository
    {
        string GenerateJWTToken(IdentityUser user, List<string> roles);
    }
}
