using System.Collections.Generic;
using System.Security.Claims;

namespace AuthenticationService.Models
{
    public interface IAuthContainerModel
    {
        public string SecretKey { get; }
        public string SecurityAlgorithm { get; }
        public int ExpiryInMinutes { get; }
        public string Issuer { get; }
        public string Audience { get; }

        public List<Claim> Claims { get; }

        public void AddClaim(Claim claim);
    }
}
