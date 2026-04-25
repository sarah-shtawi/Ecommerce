using Ecommerce12.DAL.DTO_s.Request.AuthenticationRequest;
using Ecommerce12.DAL.DTO_s.Response.AuthenticationResponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce12.BLL.Service
{
    public  interface IAuthenticationService
    {
        public Task<RegisterResponse> RegisterAsync(RegisterRequest RegiserReq);

        public Task<LoginResponse> LoginAsync (LoginRequest LoginReq);

        public Task<bool> ConfirmEmailAsync(string token, string userId);

        Task<LoginResponse> RefreshTokenAsync(TokenApiModel request);
        Task<ForgetPasswordResponse> forgetPassword(ForgetPasswordRequest request);
        Task<ResetPasswordResponse> ResetPassword(ResetPasswordRequest request);

    }
}
