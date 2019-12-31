using System;
using System.Text;
using KBS.Web.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace KBS.Web.Infrastructure {
    public static class JwtExtension {
        public static IServiceCollection AddJwt(this IServiceCollection services, IConfiguration configuration) {

            services.Configure<JwtConfig> (configuration.GetSection ("jwtConfig"));

            var token = configuration.GetSection ("jwtConfig").Get<JwtConfig> ( );

            Console.WriteLine ($"Token secret: {token.Secret}");

            services.AddAuthentication (x => {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer (x => {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey (Encoding.ASCII.GetBytes (token.Secret)),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });

            services.AddScoped<IAuthenticateService, TokenAuthenticateService> ( );

            return services;
        }
    }
}
