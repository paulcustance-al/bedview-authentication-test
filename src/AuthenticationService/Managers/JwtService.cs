using AuthenticationService.Models;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace AuthenticationService.Managers
{
    public class JwtService : IAuthService
    {
        public readonly IAuthContainerModel _authModel;

        public JwtService(IAuthContainerModel authModel)
        {
            _authModel = authModel;
        }

        public string GenerateToken(IAuthContainerModel model)
        {
            if (model == null || model.Claims == null || model.Claims.Count == 0)
                throw new ArgumentNullException(nameof(model));

            var securityTokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(model.Claims),
                Expires = DateTime.UtcNow.AddMinutes(Convert.ToInt32(model.ExpiryInMinutes)),
                Audience = _authModel.Audience,
                Issuer = _authModel.Issuer,
                SigningCredentials = new SigningCredentials(GetSymmetricSiginingKey(), model.SigningAlgorithm),
                EncryptingCredentials = new EncryptingCredentials(GetSymmetricEncryptionKey(), model.EncryptionAlgorithm, model.EncryptionEncoding)
            };

            var jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
            var securityToken = jwtSecurityTokenHandler.CreateToken(securityTokenDescriptor);
            var token = jwtSecurityTokenHandler.WriteToken(securityToken);

            return token;
        }

        private SecurityKey GetSymmetricSiginingKey() 
        {
            var symmetricKey = Encoding.ASCII.GetBytes(_authModel.SigningKey);
            return new SymmetricSecurityKey(symmetricKey);
        }

        private SymmetricSecurityKey GetSymmetricEncryptionKey()
        {
            var symmetricKey = Encoding.ASCII.GetBytes(_authModel.EncryptionKey);
            return new SymmetricSecurityKey(symmetricKey);
        }
    }
}
