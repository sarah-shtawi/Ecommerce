using Ecommerce12.DAL.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace Ecommerce12.DAL.Utils
{
    public class UserSeedData : ISeedData
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public UserSeedData(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task DataSeed()
        {
            if (!await _userManager.Users.AnyAsync())
            { // Add Data
              var user1 = new ApplicationUser {
                  UserName = "SarahSHtawi1",
                  Email ="sarah@gmail.com",
                  FullName = "sarah jalal shatwi",
                  EmailConfirmed = true 
              };
                var user2 = new ApplicationUser 
                { UserName = "Doaa.Shtawi12", 
                    Email = "Doaa@gmail.com",
                    FullName = "Doaa jalal shatwi",
                    EmailConfirmed = true }; 
                var user3 = new ApplicationUser
                { UserName = "Ahmad.Shtawi123",
                    Email = "Ahmad@gmail.com",
                    FullName = "Ahmad jalal shatwi",
                    EmailConfirmed = true 
                }; 
                // Add to Data Base
                await _userManager.CreateAsync(user1, "Sarah.com@123");
                await _userManager.CreateAsync(user2, "Doaa.com@123"); 
                await _userManager.CreateAsync(user3, "Ahmad.com@123"); 
                // Add Roles to user
                await _userManager.AddToRoleAsync(user1,"SuperAdmin");
                await _userManager.AddToRoleAsync(user2, "Admin"); 
                await _userManager.AddToRoleAsync(user3, "User");
            
            }
        }
    } 
}


            
