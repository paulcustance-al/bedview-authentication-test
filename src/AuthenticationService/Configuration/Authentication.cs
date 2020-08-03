namespace AuthenticationService.Configuration
{
    public class AuthenticationConfiguration
    {
        public string SecurityAlgorithm { get; set; }
        public string SigningKey { get; set; }
        public string EncryptionKey { get; set; }
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public int ExpiryInMinutes { get; set; }
    }
}
