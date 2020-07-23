using AuthenticationService.Models;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace AuthenticationService.Managers
{
    public class JwtService : IAuthService
    {
        public string SecretKey { get; }

        public JwtService(string secretKey)
        {
            SecretKey = secretKey;
        }

        public string GenerateToken(IAuthContainerModel model)
        {
            if (model == null || model.Claims == null || model.Claims.Length == 0)
                throw new ArgumentNullException(nameof(model));

            var securityTokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(model.Claims),
                Expires = DateTime.UtcNow.AddMinutes(Convert.ToInt32(model.ExpireMinutes)),
                SigningCredentials = new SigningCredentials(GetSymmetricSecurityKey(), model.SecurityAlgorithm)
            };

            var jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
            var securityToken = jwtSecurityTokenHandler.CreateToken(securityTokenDescriptor);
            var token = jwtSecurityTokenHandler.WriteToken(securityToken);

            return token;
        }

        public IEnumerable<Claim> GetTokenClaims(string token)
        {
            if (string.IsNullOrEmpty(token)) throw new ArgumentNullException(nameof(token));

            var tokenValidationParameters = GetTokenValidationParameters();
            var jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
            var claimsPrincipal = jwtSecurityTokenHandler.ValidateToken(token, tokenValidationParameters, out _);
            return claimsPrincipal.Claims;
        }

        public bool IsTokenValid(string token)
        {
            if (string.IsNullOrWhiteSpace(token)) throw new ArgumentNullException(nameof(token));

            var tokenValidationParameters = GetTokenValidationParameters();
            var jwtSecurityTokenHandler = new JwtSecurityTokenHandler();

            try
            {
                var claimsPrincipal = jwtSecurityTokenHandler.ValidateToken(token, tokenValidationParameters, out var validatedToken);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        private SecurityKey GetSymmetricSecurityKey() 
        {
            var symmetricKey = Convert.FromBase64String(SecretKey);
            return new SymmetricSecurityKey(symmetricKey);
        }

        private TokenValidationParameters GetTokenValidationParameters() 
        {
            return new TokenValidationParameters()
            {
                ValidateIssuer = false,
                ValidateAudience = false,
                ValidateLifetime = true,
                IssuerSigningKey = GetSymmetricSecurityKey(),
                ClockSkew = TimeSpan.Zero
            };
        }
    }
}
