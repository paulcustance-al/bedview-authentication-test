using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;

namespace AuthenticationService.Models
{
    public class JwtContainerModel : IAuthContainerModel
    {
        public int ExpireMinutes { get; set; } = 60;
        public string SecretKey { get; set; } = "p7ThCIQQkDT1HAPNcZNzboNYArsxQqWg";
        public string SecurityAlgorithm { get; set; } = SecurityAlgorithms.HmacSha256Signature;

        public Claim[] Claims { get; set; }
    }
}
