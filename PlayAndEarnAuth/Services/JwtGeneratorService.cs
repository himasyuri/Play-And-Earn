using Auth.Common;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using PlayAndEarnAuth.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;

namespace PlayAndEarnAuth.Services
{
    public class JwtGeneratorService : IJwtGeneratorService
    {
        private readonly IOptions<AuthOptions> _authOptions;
        private readonly UserManager<User> _userNanager;

        public JwtGeneratorService(IOptions<AuthOptions> authOptions, UserManager<User> userNanager)
        {
            _authOptions = authOptions;
            _userNanager = userNanager;
        }

        public async ValueTask<string> CreateAccessToken(User user)
        {
            var authParams = _authOptions.Value;
            var sequrityKey = authParams.GetSymmetricSecurityKey();
            var creditionals = new SigningCredentials(sequrityKey, SecurityAlgorithms.HmacSha256);
            var claims = new List<Claim>() {
                new Claim(JwtRegisteredClaimNames.NameId, user.Id.ToString())

               // new Claim(JwtRegisteredClaimNames.GivenName, user.UserName)
            };
            claims.Add(new Claim("Username", user.UserName));
            var userRoles = await _userNanager.GetRolesAsync(user);
            
            foreach (var role in userRoles)
            {
                claims.Add(new Claim("roles", role));
            }
            //TODO: access token, refresh token
            var token = new JwtSecurityToken(authParams.Issuer,
                authParams.Audience,
                claims,
                expires: DateTime.Now.AddDays(authParams.TokenLifeTime),
                signingCredentials: creditionals);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
