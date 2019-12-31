using KBS.Web.Model;
using KBS.Web.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace KBS.Web.Controllers {
    [ApiController]
    public class AuthController : Controller {

        IAuthenticateService authenticateService;

        public AuthController(IAuthenticateService authenticateService) {
            this.authenticateService = authenticateService;
        }

        [Route ("api/authorize")]
        [HttpPost]
        [AllowAnonymous]
        public IActionResult Login(AuthRequest request) {

            AuthResponse result;

            var authenticated = authenticateService.IsAuthenticated (request, out result);

            if (authenticated) {

                Response.Headers.Add ("X-Role", result.Role);
                return Ok (result);
            }
            else {
                return Unauthorized (new { Error = "username or password invalid" });
            }
        }
    }
}
