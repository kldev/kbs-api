using System.IO;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace KBS.Web {
    public class Program {
        public static void Main(string[] args) {
            var host = new HostBuilder ( )
                .UseContentRoot (Directory.GetCurrentDirectory ( ))
                .ConfigureWebHostDefaults (webBuilder => {
                    webBuilder.UseKestrel (serverOptions => {
                        // Set properties and call methods on options
                    })
                        //.UseIISIntegration()
                        .UseStartup<Startup> ( );
                })
                .Build ( );

            host.Run ( );
        }

    }
}
