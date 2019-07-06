using PDX.BLL.Services.Interfaces;
using Microsoft.Extensions.Options;
using System;
using System.Linq;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace PDX.API.Middlewares.Token
{
    public class TokenProvider
    {
        private readonly IAccountService _accountService;
        private readonly IUserService _userService;
        private readonly TokenProviderOptions _options;

        /// <summary>
        /// Constructor for TokenProvider
        /// </summary>
        /// <param name="options"></param>
        /// <param name="accountService"></param>
        public TokenProvider(IOptions<TokenProviderOptions> options, IAccountService accountService, IUserService userService)
        {
            _accountService  = accountService;
            _userService = userService;
            _options = options.Value;
            ThrowIfInvalidOptions(_options);
        }
        /// <summary>
        /// Generate Token based on given credential 
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public async Task<TokenResult> GenerateToken(string username, string password )
        {
           var isAuthenticated = await  _accountService.AuthenticateUserAsync(username, password);
            if (!isAuthenticated)
            {
                return null;
            }

            var now = DateTime.UtcNow;

            // Specifically add the jti (nonce), iat (issued timestamp), and sub (subject/user) claims.
            // You can add other claims here, if you want:
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, username),
                new Claim(JwtRegisteredClaimNames.Jti, await _options.NonceGenerator()),
                new Claim(JwtRegisteredClaimNames.Iat, ToUnixEpochDate(now).ToString(), ClaimValueTypes.Integer64)
            };

            //Attach Claims with type or Role
            var user = await _userService.GetUserByUsername(username);
            foreach(var role in user.Roles){
                claims.Add(new Claim(ClaimTypes.Role, role.Name) );
            }

            //Attach UserID as claim
            claims.Add(new Claim(ClaimTypes.PrimarySid, Convert.ToString(user.ID)));

            // Create the JWT and write it to a string
            var jwt = new JwtSecurityToken(
                issuer: _options.Issuer,
                audience: _options.Audience,
                claims: claims,
                notBefore: now,
                expires: now.Add(_options.Expiration),
                signingCredentials: _options.SigningCredentials);

            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

            var response = new TokenResult
            {
                access_token = encodedJwt,
                expires_in = (int)_options.Expiration.TotalSeconds
            };

            return response;
        }

        private static void ThrowIfInvalidOptions(TokenProviderOptions options)
        {
            if (string.IsNullOrEmpty(options.Path))
            {
                throw new ArgumentNullException(nameof(TokenProviderOptions.Path));
            }

            if (string.IsNullOrEmpty(options.Issuer))
            {
                throw new ArgumentNullException(nameof(TokenProviderOptions.Issuer));
            }

            if (string.IsNullOrEmpty(options.Audience))
            {
                throw new ArgumentNullException(nameof(TokenProviderOptions.Audience));
            }

            if (options.Expiration == TimeSpan.Zero)
            {
                throw new ArgumentException("Must be a non-zero TimeSpan.", nameof(TokenProviderOptions.Expiration));
            }

            if (options.SigningCredentials == null)
            {
                throw new ArgumentNullException(nameof(TokenProviderOptions.SigningCredentials));
            }

            if (options.NonceGenerator == null)
            {
                throw new ArgumentNullException(nameof(TokenProviderOptions.NonceGenerator));
            }
        }


        /// <summary>
        /// Get this datetime as a Unix epoch timestamp (seconds since Jan 1, 1970, midnight UTC).
        /// </summary>
        /// <param name="date">The date to convert.</param>
        /// <returns>Seconds since Unix epoch.</returns>
        public static long ToUnixEpochDate(DateTime date) => new DateTimeOffset(date).ToUniversalTime().ToUnixTimeSeconds();
        
    }
}