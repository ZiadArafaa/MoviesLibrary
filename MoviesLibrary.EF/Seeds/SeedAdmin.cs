using MoviesLibrary.Core.Consts;
using MoviesLibrary.Core.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoviesLibrary.EF.Seeds
{
    public class SeedAdmin
    {
        private readonly UserManager<ApplicationUser> _userManager;
        public SeedAdmin(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }
        public async Task SeedAsync()
        {
            if (!_userManager.Users.Any())
            {
                var User = new ApplicationUser { Email = "Admin@tst.com", UserName = "Admin" };
                await _userManager.CreateAsync(User, "P@ssword123");
                await _userManager.AddToRoleAsync(User, AppRole.Admin);
            }
        }
    }
}
