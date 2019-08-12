using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace KBS.Web.Infrastructure
{
    public static class CorsExtension
    {
        public readonly static string PolicyName = "corsPolicy";

        public static IServiceCollection AddAppCors(this IServiceCollection services, IConfiguration configuration)
        {

            services.Configure<CorsConfig>(configuration.GetSection("corsConfig"));

            var config = configuration.GetSection("corsConfig").Get<CorsConfig>();

            System.Console.WriteLine("Allowed Cors: " + string.Join(",", config.Allowed));

            services.AddCors(options =>
            {
                options.AddPolicy(PolicyName, cors =>
                   // TODO move to configuration
                   cors.WithOrigins(config.Allowed)
                   .AllowAnyMethod()
                   .AllowAnyHeader()
                   .AllowCredentials());
            });


            return services;
        }

    }
}