using AuthenticationService.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Security.Claims;

namespace AuthenticationService.Models
{
    public class JwtContainerModel : IAuthContainerModel
    {
        private readonly AuthenticationConfiguration _authenticationConfiguration;

        public JwtContainerModel(IOptions<AuthenticationConfiguration> authenticationConfiguration)
        {
            _authenticationConfiguration = authenticationConfiguration.Value;

            SecretKey = _authenticationConfiguration.SecretKey;
            Issuer = _authenticationConfiguration.Issuer;
            Audience = _authenticationConfiguration.Audience;
            ExpiryInMinutes = _authenticationConfiguration.ExpiryInMinutes;
            Claims = new List<Claim>();
        }

        public int ExpiryInMinutes { get; } = 60;
        public string SecurityAlgorithm { get; set; } = SecurityAlgorithms.HmacSha256Signature;
        public string SecretKey { get; }
        public string Issuer { get; }
        public string Audience { get; }
        public List<Claim> Claims { get; }

        public void AddClaim(Claim claim)
        {
            Claims.Add(claim);
        }
    }
}
