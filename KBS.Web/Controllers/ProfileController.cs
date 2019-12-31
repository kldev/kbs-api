using System;
using KBS.Web.Infrastructure;
using KBS.Web.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static KBS.Web.Infrastructure.Roles;

namespace KBS.Web.Controllers {


    [Authorize (Roles = Roles.OwnerAndSalesman)]
    [Route ("api/profile/[action]")]
    public class ProfileController : KbsController {

        [HttpGet]
        public IActionResult Get() {
            throw new NotImplementedException ( );
        }

        [HttpPost]
        public IActionResult Update(UpdateProfileRequest model) {
            throw new NotImplementedException ( );
        }
    }
}
