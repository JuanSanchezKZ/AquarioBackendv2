using Microsoft.AspNetCore.Identity;

namespace AquarioBackend.Repositories
{
    public interface ITokenRepository
    {
        string CreateJWTToken(IdentityUser user, List<string> roles);
    }
}
