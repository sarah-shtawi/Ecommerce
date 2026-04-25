using Ecommerce12.DAL.DTO_s.Request.User;
using Ecommerce12.DAL.DTO_s.Response.AuthenticationResponse;
using Ecommerce12.DAL.DTO_s.Response.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce12.BLL.Service
{
    public interface IManageUserService
    {
        Task<List<UserListResponse>> GetUsers();
        Task <UserDetailsResponse> GetUserDetails (string userId);

        Task<BaseResponse> BlockedUser(string userId);

        Task<BaseResponse> UnBlockedUser(string userId);
        Task<BaseResponse> ChangeUserRole(string userId, ChangeUserRoleRequest request);

    }
}
