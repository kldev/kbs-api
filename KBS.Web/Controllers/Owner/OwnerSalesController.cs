using System;
using System.Data;
using System.Threading.Tasks;
using KBS.Web.Data;
using KBS.Web.Data.Poco;
using KBS.Web.Infrastructure;
using KBS.Web.Model;
using KBS.Web.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace KBS.Web.Controllers.Owner
{
    [Authorize(Policy = Roles.Owner)]
    [Route("api/owner/salesman/[action]")]
    public class OwnerSalesController : KbsController
    {
        private readonly OwnerSalesRepository _ownerSalesRepository;
        private readonly UserRepository _userRepository;
        

       
        public OwnerSalesController(IConnectionFactory factory, IPasswordService passwordService)
        {
            _ownerSalesRepository = new OwnerSalesRepository(factory);
            _userRepository = new UserRepository(factory, passwordService);
        }
        
        [HttpGet]
        public async Task<IActionResult> List()
        {
            var list = await _ownerSalesRepository.List();

            return Ok(list);
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddUserRequest model)
        {
            
            var record = await _userRepository.Add(model, UserRoleEnum.Salesman);
            
            return Ok(new { userId = record});
        }
        
        [HttpPost]
        public async Task<IActionResult> Update(UpdateProfileRequest model)
        {

            var record = await _userRepository.Update(model);
            
            return Ok();
        }
        
        [HttpPost]
        public async Task<IActionResult> Remove(DeleteUserRequest model)
        {

            var record = await _userRepository.Delete(model);
            
            return Ok();
        }
    }
}