using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using RestAPI.Example.Application.Models;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace RestAPI.Example.Application.Helper
{
    public class TokenGenerator(IConfiguration configuration)
    {
        private readonly string TokenSecret = configuration["JWT:Key"];
        private readonly TimeSpan TokenLifetime = TimeSpan.FromMinutes(Convert.ToDouble(configuration["JWT:ExpiryTime"]));

        public string GenerateToken(User user, IEnumerable<string> userCalims)
        {

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(TokenSecret);

            var claims = new List<Claim>
            {
                new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new(JwtRegisteredClaimNames.Sub, user.Email),
                new(JwtRegisteredClaimNames.Email, user.Email),
                new("userid", user.Id.ToString())
            };

            foreach (var claim in userCalims)
            {
                var userClaim = new Claim(claim, "true", ClaimValueTypes.Boolean);
                claims.Add(userClaim);
            }

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.Add(TokenLifetime),
                Issuer = configuration["JWT:Issuer"],
                Audience = configuration["JWT:Audience"],
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            var jwt = tokenHandler.WriteToken(token);
            return jwt;
        }
    }
}
