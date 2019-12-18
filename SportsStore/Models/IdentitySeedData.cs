using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportsStore.Models
{
    public static class IdentitySeedData
    {
        /*
         * 1. create the user 
         * 2. create the role
         * 3. add the user to this role
         * 
         */
        private const string adminUser = "John";
        private const string adminPassword = "Secret123$";

        private const string managerUser = "Paul";
        private const string managerPassword = "Secret456$";

        private const string adminRoleName = "Admin";
        private const string managerRoleName = "Manager";

        public static async void EnsurePopulated(IApplicationBuilder app)
        {
            //create the role
            RoleManager<IdentityRole> roleManager = app.ApplicationServices
                .GetRequiredService<RoleManager<IdentityRole>>();

            //1. create admin role
            IdentityRole adminRole = await roleManager.FindByNameAsync(adminRoleName);

            if (adminRole == null)
            {
                adminRole = new IdentityRole(adminRoleName);
                await roleManager.CreateAsync(adminRole);
            }

            // this is the service that we can use the user service
            UserManager<IdentityUser> userManager = app.ApplicationServices
                .GetRequiredService<UserManager<IdentityUser>>();

            //2. create the admin user 
            // await is required async method to be declared 
            IdentityUser user = await userManager.FindByIdAsync(adminUser);

            if (user == null)
            {
                user = new IdentityUser(adminUser);
                await userManager.CreateAsync(user, adminPassword);
                //3. assign admin to admin role
                await userManager.AddToRoleAsync(user, adminRoleName);
            }
            else
            {
                if (!(await userManager.IsInRoleAsync(user, adminRoleName)))
                {
                    await userManager.AddToRoleAsync(user, adminRoleName);
                }
            }

            //1. create manager role
            IdentityRole managerRole = await roleManager.FindByNameAsync(managerRoleName);

            if (managerRole == null)
            {
                managerRole = new IdentityRole(managerRoleName);
                //2. save to the database 
                await roleManager.CreateAsync(managerRole);
            }

            //3. create the manager user 
            // await is required async method to be declared 
            IdentityUser managerUserIdentity = await userManager.FindByNameAsync(managerUser);

            if (managerUserIdentity == null)
            {
                managerUserIdentity = new IdentityUser(managerUser);
                //4. save it
                await userManager.CreateAsync(managerUserIdentity, managerPassword);
                //5. add manager user to the manager role
                await userManager.AddToRoleAsync(managerUserIdentity, managerRoleName);
            }
            else
            {
                if (!(await userManager.IsInRoleAsync(managerUserIdentity, managerRoleName)))
                {
                    await userManager.AddToRoleAsync(managerUserIdentity, managerRoleName);
                }
            }

        }
    }
}
