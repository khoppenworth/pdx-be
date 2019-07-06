using System.Text;
using Microsoft.AspNetCore.Builder;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Options;
using PDX.API.Middlewares.Token;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace PDX.API
{
    public partial class Startup
    {

        private void ConfigureAuth(IServiceCollection services)
        {

            services.AddAuthentication(o =>
                {
                    o.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    o.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                }).AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = GetTokenValidationParameters();
                    options.RequireHttpsMetadata = false;
                });

        }

        private TokenProviderOptions GetTokenProviderOptions(){
            //JWT Configuration
            var jwtConfig = Configuration.GetSection("JwtOptions");

            var jwtOptions = new TokenProviderOptions
            {
                Audience = jwtConfig["Audience"],
                Issuer = jwtConfig["Issuer"],
                SecretKey = jwtConfig["SecretKey"]
            };
            return jwtOptions;
        }

        private TokenValidationParameters GetTokenValidationParameters()
        {
            
            var jwtOptions = GetTokenProviderOptions();

            var tokenValidationParameters = new TokenValidationParameters
            {
                // The signing key must match!
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = jwtOptions.SymmetricSecurityKey,

                // Validate the JWT Issuer (iss) claim
                ValidateIssuer = true,
                ValidIssuer = jwtOptions.Issuer,

                // Validate the JWT Audience (aud) claim
                ValidateAudience = true,
                ValidAudience = jwtOptions.Audience,

                // Validate the token expiry
                ValidateLifetime = true,

                // If you want to allow a certain amount of clock drift, set that here:
                ClockSkew = System.TimeSpan.Zero
            };

            return tokenValidationParameters;
        }

        private void ConfigurePolicies(IServiceCollection services)
        {
            //CORS
            services.AddCors(o => o.AddPolicy("CorsPolicy", builder =>
            {
                builder.AllowAnyOrigin()
                       .AllowAnyMethod()
                       .AllowAnyHeader();
            }));

            //Authorization Policies
            services.AddAuthorization(options =>
            {
                options.AddPolicy("RequireElevatedRights", policy => policy.RequireRole("Super Admin"));
                options.AddPolicy("RequireAdminRights", policy => policy.RequireRole("Super Admin", "Administrator"));
            });
        }
    }
}