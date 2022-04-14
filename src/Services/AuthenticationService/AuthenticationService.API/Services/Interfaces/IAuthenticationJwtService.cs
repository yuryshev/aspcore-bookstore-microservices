using System.IdentityModel.Tokens.Jwt;

namespace AuthenticationService.API.Services.Interfaces
{
    public interface IAuthenticationJwtService
    {
        public string GetJwtAccessToken(string username);
    }
}
