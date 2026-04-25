using Ecommerce12.BLL.Service;
using Ecommerce12.DAL.DTO_s.Request.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce12.PL.Areas.Admin
{
    [Route("api/admin/[controller]")]
    [ApiController]
    [Authorize(Roles = ("Admin"))]
    public class UsersController : ControllerBase
    {
        private readonly IManageUserService _manageUserService;

        public UsersController(IManageUserService manageUserService)
        {
            _manageUserService = manageUserService;
        }

        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            var users = await  _manageUserService.GetUsers();
            return Ok(users); 
        }

        [HttpPatch("block/{userId}")]
        public async Task<IActionResult> BlockUser([FromRoute] string userId)
        {
            return Ok( await _manageUserService.BlockedUser(userId));
        }

        [HttpPatch("unblock/{userId}")]
        public async Task<IActionResult> unBlockUser([FromRoute] string userId)
        {
            return Ok(await _manageUserService.UnBlockedUser(userId));
        }


        [HttpPatch("change-role")]
        public async Task<IActionResult> changeRole([FromQuery] string userId , [FromBody] ChangeUserRoleRequest request)
        {
            var result = await _manageUserService.ChangeUserRole(userId , request);
            return Ok(result);
        }


    }
}
