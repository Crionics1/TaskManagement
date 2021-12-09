using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TaskManagement.Services.TaskManagement.Domain.Entities;

namespace TaskManagement.Services.TaskManagement.Persistence
{
    public static class DatabaseSeed
    {
        public static async System.Threading.Tasks.Task SeedDefaultUserAsync(UserManager<ApplicationUser> userManager,RoleManager<ApplicationRole> roleManager)
        {
            var defaultUser = new ApplicationUser { Id = new Guid("07f184b3-ef9d-4644-b327-4b3bc411001e"),UserName = "test", Email = "test@gmail.com" };

            await userManager.CreateAsync(defaultUser, "123Asd!");

            await roleManager.CreateAsync(new ApplicationRole {Name = "GetTask", NormalizedName = "CreateTask" });
            await roleManager.CreateAsync(new ApplicationRole {Name = "CreateTask", NormalizedName = "CreateTask" });
            await roleManager.CreateAsync(new ApplicationRole { Name = "UpdateTask", NormalizedName = "UpdateTask" });
            await roleManager.CreateAsync(new ApplicationRole { Name = "DeleteTask", NormalizedName = "DeleteTask" });

            await userManager.AddToRoleAsync(defaultUser, "GetTask");
            await userManager.AddToRoleAsync(defaultUser, "CreateTask");
            await userManager.AddToRoleAsync(defaultUser, "UpdateTask");
            await userManager.AddToRoleAsync(defaultUser, "DeleteTask");
        }

    }
}
