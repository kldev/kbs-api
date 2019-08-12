using System;
using KBS.Web.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace KBS.Web.Controllers
{
    [ApiController]
    [Authorize]
    public abstract class KbsController : ControllerBase
    {
        [NonAction]
        protected OwnClaims CurrentUser()
        {
            return new OwnClaims
            {
                UserId = HttpContext.User?.Claims?.FirstOrDefault(x => x.Type == OwnClaims.USER_ID)?.Value,
                Role = HttpContext.User?.Claims?.FirstOrDefault(x => x.Type == OwnClaims.ROLE)?.Value,
            };
        }

        protected Guid UserId => Guid.Parse(CurrentUser()?.UserId);

        protected string Role => CurrentUser()?.Role;
    }
}