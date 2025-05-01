using Microsoft.AspNetCore.Identity;
using ValueMateApi.Constants;

namespace ValueMateApi.Data;

public static class DbInitializer
{
    public static async Task SeedAdminUserAsync(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
    {
        var adminEmail = "admin@jaggakitta.com";
        var adminPassword = "Admin@123"; // 🔐 Strong password recommended
        var roles =new string[]{ ApplicationRoleConstants.Admin, ApplicationRoleConstants.User };
        foreach (var role in roles)
        {
            // 1. Create Role if it doesn't exist
            if (!await roleManager.RoleExistsAsync(role))
            {
                await roleManager.CreateAsync(new IdentityRole(role));
            }
        }
        

        // 2. Create Admin User if it doesn't exist
        var adminUser = await userManager.FindByEmailAsync(adminEmail);
        if (adminUser == null)
        {
            adminUser = new IdentityUser
            {
                UserName = adminEmail,
                Email = adminEmail,
                EmailConfirmed = true
            };

            var result = await userManager.CreateAsync(adminUser, adminPassword);
            if (result.Succeeded)
            {
                await userManager.AddToRoleAsync(adminUser, ApplicationRoleConstants.Admin);
            }
            else
            {
                throw new Exception("Failed to create admin user: " + string.Join(", ", result.Errors.Select(e => e.Description)));
            }
        }
    }
}