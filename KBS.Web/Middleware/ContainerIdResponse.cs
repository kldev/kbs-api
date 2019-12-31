using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace KBS.Web.Middleware {
    public class ContainerIdResponse {

        private readonly RequestDelegate _next;

        public ContainerIdResponse(RequestDelegate next) {
            _next = next;
        }

        public async Task Invoke(HttpContext context) {

            context.Response.Headers.Add ("X-Machine", System.Environment.MachineName);

            await _next.Invoke (context);
        }
    }
}
