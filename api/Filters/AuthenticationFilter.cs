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
    /// <summary>
    /// Authentication filter.
    /// </summary>
    public class AuthenticationFilter : IAuthenticationFilter
    {
        private SecurityToken _validatedToken;

        /// <summary>
        /// Gets a value indicating whether this <see cref="T:API.Filters.AuthenticationFilter"/> allow multiple.
        /// </summary>
        /// <value><c>true</c> if allow multiple; otherwise, <c>false</c>.</value>
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
            string msg = "Authorization has been denied for this request.";

            if (authorization == null)
                return Task.FromResult<object>(new { msg });

            if (authorization.Scheme != "Bearer")
                return Task.FromResult<object>(new { msg });

            if (string.IsNullOrEmpty(authorization.Parameter))
                return Task.FromResult<object>(new { msg });

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

                return Task.FromResult<object>(new{ msg });
            }
            catch
            {
                return Task.FromResult<object>(new { msg });
            }

        }

        /// <summary>
        /// Challenges the async.
        /// </summary>
        /// <returns>The async.</returns>
        /// <param name="context">Context.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        public Task ChallengeAsync(HttpAuthenticationChallengeContext context, CancellationToken cancellationToken)
        {
            return Task.FromResult(0);
        }
    }
}
