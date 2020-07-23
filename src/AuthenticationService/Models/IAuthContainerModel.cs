using System.Security.Claims;

namespace AuthenticationService.Models
{
    public interface IAuthContainerModel
    {
        public string SecretKey { get; set; }
        public string SecurityAlgorithm { get; set; }
        public int ExpireMinutes { get; set; }

        public Claim[] Claims { get; set; }
    }
}
