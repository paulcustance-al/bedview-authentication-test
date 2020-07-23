using AuthenticationService.Managers;
using AuthenticationService.Models;
using System;
using System.Linq;
using System.Security.Claims;

namespace AuthenticationService
{
    class Program
    {
        static void Main(string[] args)
        {
            var model = GetJWTContainerModel("Paul Custance", "pcustance@gmail.com");
            var authService = new JwtService(model.SecretKey);
            var token = authService.GenerateToken(model);

            if (!authService.IsTokenValid(token)) throw new UnauthorizedAccessException();

            var claims = authService.GetTokenClaims(token).ToList();

            Console.WriteLine(claims.FirstOrDefault(e => e.Type.Equals(ClaimTypes.Name)).Value);
            Console.WriteLine(claims.FirstOrDefault(e => e.Type.Equals(ClaimTypes.Email)).Value);
        }

        private static JwtContainerModel GetJWTContainerModel(string name, string email)
        {
            return new JwtContainerModel()
            {
                Claims = new Claim[]
                {
                    new Claim(ClaimTypes.Name, name),
                    new Claim(ClaimTypes.Email, email)
                }
            };
        }
    }
}
