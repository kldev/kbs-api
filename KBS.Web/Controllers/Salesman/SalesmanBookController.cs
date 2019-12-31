using System.Data;
using System.Threading.Tasks;
using KBS.Web.Data;
using KBS.Web.Infrastructure;
using KBS.Web.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace KBS.Web.Controllers.Salesman {
    [Authorize (Roles = Roles.Salesman)]
    [Route ("api/salesman/book/[action]")]
    public class SalesmanBookController : KbsController {
        private readonly SalesmanBookRepository _salesmanBookRepository;

        public SalesmanBookController(IConnectionFactory factory) {
            _salesmanBookRepository = new SalesmanBookRepository (factory);
        }

        public async Task<IActionResult> List() {

            var list = await _salesmanBookRepository.List (UserId);

            return Ok (list);
        }
    }
}
