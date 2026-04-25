using Ecommerce12.DAL.DTO_s.Request.AuthenticationRequest;
using Ecommerce12.DAL.DTO_s.Response.AuthenticationResponse;
using Ecommerce12.DAL.Models;
using Mapster;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce12.BLL.Service
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IConfiguration _configration;
        private readonly IEmailSender _senderEmail;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ITokenService _tokenService;

        public AuthenticationService(UserManager<ApplicationUser> userManager , IConfiguration configration , IEmailSender senderEmail , SignInManager <ApplicationUser> signInManager, ITokenService tokenService)
        {
            _userManager = userManager;
            _configration = configration;
            _senderEmail = senderEmail;
            _signInManager = signInManager;
            _tokenService = tokenService;
        }

        public async Task<RegisterResponse> RegisterAsync(RegisterRequest RegiserReq)
        {
            try
            {
                var user = RegiserReq.Adapt<ApplicationUser>();
                var result = await _userManager.CreateAsync(user, RegiserReq.Password);
                if (!result.Succeeded)
                {
                    return new RegisterResponse()
                    {
                        Success = false,
                        Message = "User Creation Faild",
                        Errors = result.Errors.Select(e => e.Description).ToList()
                    };
                }
                await _userManager.AddToRoleAsync(user, "User");

                var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                token = Uri.EscapeDataString(token);
                var emailURL = $"https://localhost:7277/api/auth/Account/ConfirmEmail?token={token}&userId={user.Id}";
                await _senderEmail.SendEmailAsync(user.Email , "welcome" , $" <h1> welcome {user.FullName}</h1> <a href={emailURL}>Confirm Email</a>");
                
                return new RegisterResponse()
                {
                    Success = true,
                    Message = "Success"
                };
            }catch (Exception ex) 
            {
                return new RegisterResponse()
                {
                    Success = false,
                    Message = "An unexpected error",
                    Errors = new List<string> { ex.Message }
                };
            }   
        }
        public async Task<bool> ConfirmEmailAsync (string token , string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
                return false;
            else
            {
                var result = await _userManager.ConfirmEmailAsync(user , token);
                if (!result.Succeeded) 
                {
                   return false; 
                }
                return true;
            }
        }
        public async Task<LoginResponse> LoginAsync(LoginRequest LoginReq)
        {
            try
            {
                var user = await _userManager.FindByEmailAsync(LoginReq.Email);
                if (user == null)
                {
                    return new LoginResponse()
                    {
                        Success = false,
                        Message = "In Valid Email",
                    };
                }
                if(await _userManager.IsLockedOutAsync(user))
                {
                    return new LoginResponse()
                    {
                        Success = false,
                        Message = "Account is Locked , try again later"
                    };
                }
                var result = await _signInManager.CheckPasswordSignInAsync(user, LoginReq.Password, true);
                if (result.IsLockedOut)
                {
                    return new LoginResponse()
                    {
                        Success = false,
                        Message = "Account Locked due to multiple falied attempts "
                    };
                }
                else if (result.IsNotAllowed)
                {
                    return new LoginResponse()
                    {
                        Success = false,
                        Message = " please Confirm your email "
                    };
                }
                if (!result.Succeeded)
                {
                    return new LoginResponse()
                    {
                        Success = false,
                        Message = "In Valied Password"
                    };
                }

                var accessToken = await _tokenService.GenerateAccessToken(user);
                var refreshToken = _tokenService.GenerateRefreshToken();

                user.RefreshToken = refreshToken;
                user.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(7);

                await _userManager.UpdateAsync(user);

                return new LoginResponse()
                {
                    Success = true,
                    Message = "Login Successfully",
                    AccessToken = accessToken,
                    RefreshToken = refreshToken,
                };
            }
            catch (Exception ex)
            {
                return new LoginResponse()
                {
                    Success = false,
                    Message = "An unexpected error",
                    Errors = new List<string> { ex.Message }
                };
            }
        }
        public async Task<ForgetPasswordResponse> forgetPassword(ForgetPasswordRequest request)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);
            if(user is null)
            {
                return new ForgetPasswordResponse
                {
                    Success = false,
                    Message = "Email not found"
                }; 
            }
            var random = new Random();
            var code =  random.Next(1000,9999).ToString();

            user.CodeResetPassword = code;
            user.PasswordResetCodeExpiry = DateTime.UtcNow.AddMinutes(15);

            await _userManager.UpdateAsync(user);

            await _senderEmail.SendEmailAsync(request.Email , "reset password" , $" code is {code}");
            return new ForgetPasswordResponse
            {
                Success = true,
                Message = "code sent to yout email"
            };
        }

        public async Task<LoginResponse> RefreshTokenAsync(TokenApiModel request)
        {
            string accessToken = request.accessToken;
            string refreshToken = request.refreshToken;

            var principle = _tokenService.GetPrincipalFromExpiredToken(accessToken);

            var userName = principle.Identity.Name;

            var user = await _userManager.Users.FirstOrDefaultAsync(u=> u.UserName == userName);

            if(user is null || user.RefreshToken != refreshToken || user.RefreshTokenExpiryTime <= DateTime.UtcNow)
            {
                return new LoginResponse()
                {
                    Success = false,
                    Message = "Invalid client request"
                };
            }
            var newAccessToken = await _tokenService.GenerateAccessToken(user);
            var newRefreshToken =  _tokenService.GenerateRefreshToken();

            user.RefreshToken = newRefreshToken;
            await _userManager.UpdateAsync(user);

            return new LoginResponse()
            {
                Success=true,
                Message = "Token Refreshed",
                AccessToken = newAccessToken,
                RefreshToken = newRefreshToken,
            };


        }
        public async Task<ResetPasswordResponse> ResetPassword(ResetPasswordRequest request)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);
            if (user is null)
            {
                return new ResetPasswordResponse
                {
                    Success = false,
                    Message = "Email not found"
                };
            }
            else if(user.CodeResetPassword != request.code)
            {
                return new ResetPasswordResponse
                {
                    Success = false,
                    Message = "code Invalied"
                };
            }
            else if (user.PasswordResetCodeExpiry < DateTime.UtcNow)
            {
                return new ResetPasswordResponse
                {
                    Success = false,
                    Message = "code Expired"
                };
            }

            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            var result =  await _userManager.ResetPasswordAsync(user , token, request.NewPassword);
            if (!result.Succeeded)
            {
                return new ResetPasswordResponse
                {
                    Success = false,
                    Message = "reset passwprd Invalid",
                    Errors = result.Errors.Select(e => e.Description).ToList()
                };
            }
            await _senderEmail.SendEmailAsync(request.Email, "reset password", " your password is changed  ");
            return new ResetPasswordResponse
            {
                Success = true,
                Message = "password reset successfully "
            };
        }
    }
}
