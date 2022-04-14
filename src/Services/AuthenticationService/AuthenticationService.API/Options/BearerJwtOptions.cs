namespace AuthenticationService.API.Options
{
    public class BearerJwtOptions
    {
        public const string BaererJwt = "BaererJwt";

        public string Issuer { get; set; } = null!;

        public string Audience { get; set; } = null!;

        public int Expires { get; set; }

        public string SecretKey { get; set; } = null!;
    }
}
