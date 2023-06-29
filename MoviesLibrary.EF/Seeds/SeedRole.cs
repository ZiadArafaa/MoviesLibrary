using MoviesLibrary.Core.Consts;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoviesLibrary.EF.Seeds
{
    public class SeedRole
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        public SeedRole(RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
        }
        public async Task SeedAsync()
        {
            if(!_roleManager.Roles.Any()) 
            {
               await _roleManager.CreateAsync(new IdentityRole(Role.Admin));
               await _roleManager.CreateAsync(new IdentityRole(Role.User));
            }
        }
    }
}
