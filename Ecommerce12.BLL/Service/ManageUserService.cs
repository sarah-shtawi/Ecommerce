using Ecommerce12.DAL.DTO_s.Request.User;
using Ecommerce12.DAL.DTO_s.Response.AuthenticationResponse;
using Ecommerce12.DAL.DTO_s.Response.User;
using Ecommerce12.DAL.Models;
using Mapster;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce12.BLL.Service
{
    public class ManageUserService : IManageUserService
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public ManageUserService(UserManager<ApplicationUser> UserManager)
        {
            _userManager = UserManager;
        }
        public async Task<List<UserListResponse>> GetUsers()
        {
            var users =  await _userManager.Users.ToListAsync();
            var result = users.Adapt<List<UserListResponse>>();

            for (int i =0; i<users.Count; i++)
            {
                var roles = await  _userManager.GetRolesAsync(users[i]);
                result[i].Roles = roles.ToList();
            }
            return result;
        }
        public Task<UserDetailsResponse> GetUserDetails(string userId)
        {
            throw new NotImplementedException();
        }


        public async Task<BaseResponse> BlockedUser(string userId)
        {
            var user = await  _userManager.FindByIdAsync(userId);

            await _userManager.SetLockoutEnabledAsync(user,true);
            await _userManager.SetLockoutEndDateAsync(user,DateTimeOffset.MaxValue);

            await _userManager.UpdateAsync(user);
            return new BaseResponse
            {
                Success = true,
                Message = "user is blocked"
            };
        }
        public async Task<BaseResponse> UnBlockedUser(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if(user == null)
            {
                return new BaseResponse()
                {
                    Success = false,
                    Message = "user not found"
                };
            }
            await _userManager.SetLockoutEnabledAsync(user, false);
            await _userManager.SetLockoutEndDateAsync(user, null);

            await _userManager.UpdateAsync(user);
            return new BaseResponse
            {
                Success = true,
                Message = "user is unblocked"
            };
        }

        public async Task<BaseResponse> ChangeUserRole(string userId, ChangeUserRoleRequest request)
        {
            var user = await _userManager.FindByIdAsync(userId);
            var currentRole = await _userManager.GetRolesAsync(user);

            await _userManager.RemoveFromRolesAsync(user, currentRole);
            await _userManager.AddToRoleAsync(user, request.Role);
            return new BaseResponse
            {
                Success = true,
                Message = "Role updated"
            };
        }


    }
}
