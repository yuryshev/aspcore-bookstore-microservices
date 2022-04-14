using AuthenticationService.API.Options;
using AuthenticationService.API.Services;
using FluentAssertions;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Moq;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using Xunit;

namespace AuthenticationService.Tests.Tests.Services
{
    public class AuthenticationJwtServiceTests
    {
        [Fact]
        public void GetJwtAccessToken_IsValidAcessToken_ReturnsAccessToken()
        {
            // Arrange

            var bearerJwtOptions = new BearerJwtOptions
            {
                Issuer = "AuthenticationService.API",
                Audience = "Client",
                Expires = 2,
                SecretKey = "DlsAU789WPybMgbfVegdCrN4XnmAGmYx"
            };

            IConfiguration configurationMock = new ConfigurationBuilder()
                .Build();

            var authenticationService = new AuthenticationJwtService(configurationMock);

            authenticationService.BearerJwtOptions = bearerJwtOptions;


            var tokenHandler = new JwtSecurityTokenHandler();


            // Act

            string jwt = authenticationService.GetJwtAccessToken("yuryshev");

            tokenHandler.ValidateToken(jwt, new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(bearerJwtOptions.SecretKey)),
                ValidateIssuer = true,
                ValidIssuer = bearerJwtOptions.Issuer,
                ValidateAudience = true,
                ValidAudience = bearerJwtOptions.Audience
            }, out SecurityToken validatedToken);

            var jwtToken = validatedToken as JwtSecurityToken;

            var username = jwtToken!.Claims.First(x => x.Type == "username").Value;

            // Assert

            username.Should().Be("yuryshev");
        }
    }
}
