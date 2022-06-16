using backend_api.Data.Constants;
using backend_api.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace backend_api.Data
{
    public class AppDbContextSeed
    {
        public static async Task SeedEssentialsAsync(
            UserManager<User> userManager, 
            RoleManager<IdentityRole> roleManager
            )
        {
            //////Seed Roles
            //await roleManager.CreateAsync(new IdentityRole(Authorization.Roles.Administrator.ToString()));
            //await roleManager.CreateAsync(new IdentityRole(Authorization.Roles.Teamlead.ToString()));
            //await roleManager.CreateAsync(new IdentityRole(Authorization.Roles.Member.ToString()));

            //////Seed Default User
            //var defaultUser = new User { FirstName = Authorization.default_firstName, LastName = Authorization.default_lastName, UserName = Authorization.default_username, Email = Authorization.default_email, EmailConfirmed = true, PhoneNumberConfirmed = true };

            //if (!userManager.Users.Any())
            //{
            //    await userManager.CreateAsync(defaultUser, Authorization.default_password);
            //    await userManager.AddToRoleAsync(defaultUser, Authorization.default_organization_role.ToString());
            //}
        }
    }
}
