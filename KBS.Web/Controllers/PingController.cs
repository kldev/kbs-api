using System;
using Microsoft.AspNetCore.Mvc;
namespace KBS.Web.Controllers {
    [Route ("api/ping")]
    public class PingController : Controller {
        public PingController() { }

        [HttpGet]
        public IActionResult Get() {
            var machineID = System.Environment.MachineName;
            return Ok ($"pong - {machineID}");
        }

        [HttpPost]
        public IActionResult Post([FromBody]Object model) {

            return Ok (model);
        }

    }
}
