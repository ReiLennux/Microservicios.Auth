using Auth.Models;

namespace API.Services.IServices
{
    public interface IJwtTokenGenerator
    {

        string GenerateToken(ApplicationUser applicationUser, IEnumerable<string> roles);

    }
}
