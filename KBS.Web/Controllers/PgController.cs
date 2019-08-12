using System;
using System.Data;
using System.Threading.Tasks;
using KBS.Web.Data;
using KBS.Web.Services;
using Microsoft.AspNetCore.Mvc;

namespace KBS.Web.Controllers
{
    [Route("api/[controller]/[action]")]
    public class PgController : Controller
    {
        readonly CommonQueryRepository _repository;

        public PgController(IConnectionFactory factory)
        {
            _repository = new CommonQueryRepository(factory);
        }

        [HttpGet]
        public async Task<IActionResult> Now()
        {
            DateTime now = await _repository.CurrentDateAsync();
            return Ok(new { currentDate = $"{now}" });
        }

    }
}