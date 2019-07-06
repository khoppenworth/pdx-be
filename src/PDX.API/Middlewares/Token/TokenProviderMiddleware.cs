using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using PDX.BLL.Services.Interfaces;

namespace PDX.API.Middlewares.Token
{
    public class TokenProviderMiddleware
    {

        private readonly RequestDelegate _next;
        private readonly TokenProviderOptions _options;
        private readonly ILogger _logger;
        private readonly TokenProvider _tokenProvider;

        public TokenProviderMiddleware(
            RequestDelegate next,
            IOptions<TokenProviderOptions> options,
            ILoggerFactory loggerFactory,
            IAccountService accountService, IUserService userService)
        {
            _next = next;
            _options = options.Value;
            _logger = loggerFactory.CreateLogger<TokenProviderMiddleware>();
            _tokenProvider = new TokenProvider(options, accountService, userService);

        }

        public Task Invoke(HttpContext context)
        {
            // If the request path doesn't match, skip
            if (!context.Request.Path.Equals(_options.Path, StringComparison.Ordinal))
            {
                return _next(context);
            }

            // Request must be POST with Content-Type: application/x-www-form-urlencoded
            if (!context.Request.Method.Equals("POST")
               || !context.Request.HasFormContentType)
            {
                context.Response.StatusCode = 400;
                return context.Response.WriteAsync("Bad request.");
            }

            _logger.LogInformation("Handling request: " + context.Request.Path);

            return GenerateToken(context);
        }

        private async Task GenerateToken(HttpContext context)
        {
            var username = context.Request.Form["username"];
            var password = context.Request.Form["password"];

            var response = _tokenProvider.GenerateToken(username, password);
            if (response == null)
            {
                context.Response.StatusCode = 401;
                await context.Response.WriteAsync("Invalid username or password.");
                return;
            }

            // Serialize and return the response
            context.Response.ContentType = "application/json";
            await context.Response.WriteAsync(JsonConvert.SerializeObject(response, new JsonSerializerSettings{Formatting = Formatting.Indented}));
        }
    }
}