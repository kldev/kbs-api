using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KBS.Messenger.Hubs;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace KBS.Messenger {
    public class Startup {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940\


        readonly string DevPolicy = "devPolicy";

        public void ConfigureServices(IServiceCollection services) {

            services.AddCors (options => {
                options.AddPolicy (DevPolicy, builder => {
                    builder.WithOrigins ("http://localhost:8000")
                        .AllowAnyHeader ( ).AllowAnyMethod ( ).AllowCredentials ( );
                });
            });

            services.AddSignalR ( );
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env) {
            if (env.IsDevelopment ( )) {
                app.UseDeveloperExceptionPage ( );
            }

            app.UseCors (DevPolicy);
            app.UseRouting ( );

            app.UseEndpoints (endpoints => {

                endpoints.MapHub<GatewayHub> ("/cerberhub");

                endpoints.MapGet ("/", async context => {
                    await context.Response.WriteAsync ("404 nothing here");
                });



            });

        }
    }
}
