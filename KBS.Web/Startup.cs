using System;
using System.Data;
using System.IO;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using KBS.Web.Infrastructure;
using KBS.Web.Middleware;
using KBS.Web.Services;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json.Serialization;
using Npgsql;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;

namespace KBS.Web {
    public class Startup {
        public Startup(IWebHostEnvironment environment) {
            var builder = new ConfigurationBuilder ( )
               .SetBasePath (Directory.GetCurrentDirectory ( ))
               .AddJsonFile ("appsettings.json", true)
               .AddJsonFile ($"appsettings.{environment.EnvironmentName}.json", true)
               .AddEnvironmentVariables ( );

            Configuration = builder.Build ( );
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services) {
            services.AddControllers ( ).AddNewtonsoftJson (options =>
                 options.SerializerSettings.ContractResolver =
                     new CamelCasePropertyNamesContractResolver ( ));

            var mainConnectionString = Configuration["ConnectionStrings:Main"];

            if (string.IsNullOrEmpty (mainConnectionString)) throw new ArgumentException ("ConnectionStrings->Main value is missing");

            services.AddTransient<IConnectionFactory> (x => new ConnectionFactory (mainConnectionString));
            services.AddSingleton<IPasswordService, PasswordService> ( );

            services.AddJwt (Configuration);
            services.AddAppCors (Configuration);

            services.AddAuthorization (options => {
                options.AddPolicy ("Owner", policy => policy.RequireClaim ("Role", "Owner"));
                options.AddPolicy ("Salesman", policy => policy.RequireClaim ("Role", "Salesman"));
            });


        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app) {

            app.UseRouting ( );
            app.UseMachineIdMiddleware ( );
            app.UseErrorHandlingMiddleware ( );
            app.UseCors (CorsExtension.PolicyName);
            app.UseAuthentication ( );
            app.UseAuthorization ( );


            app.UseEndpoints (endpoints => {
                endpoints.MapControllers ( );
            });

        }
    }
}
