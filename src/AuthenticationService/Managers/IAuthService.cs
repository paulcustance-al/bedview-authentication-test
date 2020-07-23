using AuthenticationService.Models;
using System.Collections.Generic;
using System.Security.Claims;

namespace AuthenticationService.Managers
{
    public interface IAuthService
    {
        public string SecretKey { get; }
        public bool IsTokenValid(string token);
        public string GenerateToken(IAuthContainerModel model);
        IEnumerable<Claim> GetTokenClaims(string token);
    }
}
