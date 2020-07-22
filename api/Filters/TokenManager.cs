using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Web.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace API.Filters
{
    /// <summary>
    /// Token manager.
    /// </summary>
    public static class TokenManager
    {
        /// <summary>
        /// The secret.
        /// </summary>
        public static string Secret = WebConfigurationManager.AppSettings["JWTToken"];

        /// <summary>
        /// Generates the token.
        /// </summary>
        /// <returns>The token.</returns>
        /// <param name="userName">User name.</param>
        public static string GenerateToken(string userName, string role)
        {
            byte[] key = Convert.FromBase64String(Secret);
            SymmetricSecurityKey securityKey = new SymmetricSecurityKey(key);
            SecurityTokenDescriptor descriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] { 
                    new Claim(ClaimTypes.Name, userName),
                    new Claim(ClaimTypes.Role, role)
                }),
                Expires = DateTime.UtcNow.AddDays(1),
                SigningCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature)
            };
            JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();
            JwtSecurityToken token = handler.CreateJwtSecurityToken(descriptor);
            return handler.WriteToken(token);
        }
    }
}
