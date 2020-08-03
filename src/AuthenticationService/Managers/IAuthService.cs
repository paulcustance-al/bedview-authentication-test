using AuthenticationService.Models;

namespace AuthenticationService.Managers
{
    public interface IAuthService
    {
        public string GenerateToken(IAuthContainerModel model);
    }
}
