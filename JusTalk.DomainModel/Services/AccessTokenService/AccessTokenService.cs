using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Threading.Tasks;
using JusTalk.DAL;
using JusTalk.DomainModel.Options;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace JusTalk.DomainModel
{
    public class AccessTokenService : IAccessTokenService
    {
        private readonly JwtAuthOptions _jwtAuthOptions;
        
        public AccessTokenService(IOptions<JwtAuthOptions> jwtAuthOptions)
        {
            _jwtAuthOptions = (jwtAuthOptions != null) ? jwtAuthOptions.Value : throw new ArgumentNullException(nameof(jwtAuthOptions));
        }

        public string GenerateAccessToken(User user)
        {
            var userClaims = GetClaimsIdentity(user);
            var accessToken = GenerateAccessToken(userClaims, user.Id);

            return accessToken;
        }
        
        private string GenerateAccessToken(ClaimsIdentity claimsIdentity, string id)
        {
            if (claimsIdentity == null) throw new ArgumentException(nameof(claimsIdentity));

            var tokenDescriptor = new SecurityTokenDescriptor()
            {
                Subject = claimsIdentity,
                Audience = _jwtAuthOptions.JwtAudience,
                Issuer = _jwtAuthOptions.JwtIssuer,
                Expires = DateTime.UtcNow.AddMinutes(_jwtAuthOptions.LifeTime),
                SigningCredentials = new SigningCredentials(_jwtAuthOptions.SymmetricSecurityKey, SecurityAlgorithms.HmacSha256Signature)
            };
            
            var tokenHandler = new JwtSecurityTokenHandler();
            var securityToken = tokenHandler.CreateToken(tokenDescriptor);
            var token = tokenHandler.WriteToken(securityToken);

            return token;
        }
        
        private ClaimsIdentity GetClaimsIdentity(User user)
        {
            if (user == null) throw new ArgumentNullException(nameof(user));

            var claims = new List<Claim>()
            {
                new Claim("id", user.Id),
                new Claim(ClaimTypes.MobilePhone, user.Phone)
            };
            
            return new ClaimsIdentity(claims, "token");
        }
    }
}