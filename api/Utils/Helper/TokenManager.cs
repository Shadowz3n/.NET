using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Web.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace API.Utils.Helper
{
    /// <summary>
    /// Token manager.
    /// </summary>
    public class TokenManager
    {
        /// <summary>
        /// The secret.
        /// </summary>
        public static string _secret = WebConfigurationManager.AppSettings["JWTToken"];

        /// <summary>
        /// Generates the token.
        /// </summary>
        /// <returns>The token.</returns>
        /// <param name="claims">Claims.</param>
        public string Generate(Claim[] claims)
        {
            byte[] key = Convert.FromBase64String(_secret);
            SymmetricSecurityKey securityKey = new SymmetricSecurityKey(key);
            SecurityTokenDescriptor descriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddDays(1),
                SigningCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature)
            };
            JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();
            JwtSecurityToken token = handler.CreateJwtSecurityToken(descriptor);
            return handler.WriteToken(token);
        }
    }
}
