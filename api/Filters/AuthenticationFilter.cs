using System;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Configuration;
using System.Web.Http.Filters;
using Microsoft.IdentityModel.Tokens;

namespace API.Filters
{
    public class AuthenticationFilter : IAuthenticationFilter
    {
        private SecurityToken _validatedToken;

        public bool AllowMultiple
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        Task IAuthenticationFilter.AuthenticateAsync(HttpAuthenticationContext context, CancellationToken cancellationToken)
        {

            var request = context.Request;
            var authorization = request.Headers.Authorization;
            string unauthorizedMsg = "Authorization has been denied for this request.";

            if (authorization == null)
            {
                return Task.FromResult<object>(new
                {
                    msg = unauthorizedMsg
                });
            }

            if (authorization.Scheme != "Bearer")
            {
                return Task.FromResult<object>(new
                {
                    msg = unauthorizedMsg
                });
            }

            if (string.IsNullOrEmpty(authorization.Parameter))
            {
                return Task.FromResult<object>(new
                {
                    msg = unauthorizedMsg
                });
            }

            var tokenHandler = new JwtSecurityTokenHandler();

            byte[] key = Encoding.ASCII.GetBytes(WebConfigurationManager.AppSettings["JWTToken"]);

            try
            {
                var claims = tokenHandler.ValidateToken(authorization.Parameter, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false
                }, out _validatedToken);

                context.Principal = claims;

                return Task.FromResult<object>(new
                {
                    msg = unauthorizedMsg
                });
            }
            catch
            {
                return Task.FromResult<object>(new
                {
                    msg = unauthorizedMsg
                });
            }

        }

        public Task ChallengeAsync(HttpAuthenticationChallengeContext context, CancellationToken cancellationToken)
        {
            return Task.FromResult(0);
        }
    }
}
