using AuthenticationService.Models;
using System.Security.Claims;

namespace AuthenticationService.Managers
{
    public interface IAuthService
    {
        public string GenerateToken(IAuthContainerModel model);
        public ClaimsPrincipal DecryptToken(IAuthContainerModel model, string token);
    }
}
