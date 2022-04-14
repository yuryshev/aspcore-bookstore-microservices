using AuthenticationService.API.Options;
using AuthenticationService.API.Services.Interfaces;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace AuthenticationService.API.Services
{
    public class AuthenticationJwtService : IAuthenticationJwtService
    {
        private readonly IConfiguration _configuration;

        public BearerJwtOptions BearerJwtOptions { get; set; }

        public AuthenticationJwtService(IConfiguration configuration)
        {
            _configuration = configuration;

            BearerJwtOptions = new BearerJwtOptions();

            _configuration.GetSection(BearerJwtOptions.BaererJwt).Bind(BearerJwtOptions);
        }

        public string GetJwtAccessToken(string username)
        {

            var claims = new List<Claim>
            {
                new Claim("username", username)
            };

            var jwt = new JwtSecurityToken(
                issuer: BearerJwtOptions.Issuer,
                audience: BearerJwtOptions.Audience,
                claims: claims,
                expires: DateTime.UtcNow.Add(TimeSpan.FromMinutes(BearerJwtOptions.Expires)),
                signingCredentials:
                    new SigningCredentials(
                            new SymmetricSecurityKey(Encoding.UTF8.GetBytes(BearerJwtOptions.SecretKey)),
                            SecurityAlgorithms.HmacSha256
                    )
            );

            return new JwtSecurityTokenHandler().WriteToken(jwt);
        }
    }
}
