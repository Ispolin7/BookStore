using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;

namespace BookStore.Services.Auth
{
    public class TokenFactory
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        private ClaimsIdentity GetClaims(IdentityUser user, IEnumerable<string> roles)
        {
            //user.
            var claims = new List<Claim>
            {
                new Claim("user_name", user.UserName),
                new Claim("id", user.Id),
            };

            roles.ToList().ForEach(r => claims.Add(new Claim(ClaimsIdentity.DefaultRoleClaimType, r)));

            ClaimsIdentity claimsIdentity = new ClaimsIdentity(
                claims,
                "Token",
                ClaimsIdentity.DefaultNameClaimType,
                ClaimsIdentity.DefaultRoleClaimType);

            return claimsIdentity;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public string GenerateToken(IdentityUser user, IEnumerable<string> roles)
        {
            var token = new JwtSecurityToken(
                    issuer: AuthOptions.Issuer,
                    audience: AuthOptions.Audience,
                    //notBefore: now,
                    //expires: now.Add(TimeSpan.FromMinutes(AuthOptions.LIFETIME)),
                    claims: GetClaims(user, roles).Claims,
                    signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
