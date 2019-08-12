using System;
using Microsoft.AspNetCore.Mvc;
namespace KBS.Web.Controllers
{
    [Route("api/ping")]
    public class PingController : Controller
    {
        public PingController() { }

        [HttpGet]
        public IActionResult Get()
        {
            
            return Ok("pong");
        }
        
        [HttpPost]
        public IActionResult Post([FromBody]Object model)
        {
            
            return Ok(model);
        }

    }
}