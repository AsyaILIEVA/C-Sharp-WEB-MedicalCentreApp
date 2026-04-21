using MedicalCentreApp.Data.Models;
using Microsoft.AspNetCore.Identity;

namespace MedicalCentreApp.Extensions
{
    public static class ApplicationBuilderExtensions
    {
        public static async Task SeedApplicationAsync(this IApplicationBuilder app)
        {
            using var scope = app.ApplicationServices.CreateScope();
            var services = scope.ServiceProvider;

            var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();

            await RoleSeeder.SeedAsync(services);
            await SeedAdminAsync(userManager);            
        }

        private static async Task SeedAdminAsync(UserManager<ApplicationUser> userManager)
        {
            string adminEmail = "admin@medicalcentre.com";
            string adminPassword = "Admin123!";

            var adminUser = await userManager.FindByEmailAsync(adminEmail);

            if (adminUser == null)
            {
                adminUser = new ApplicationUser
                {
                    UserName = adminEmail,
                    Email = adminEmail,
                    EmailConfirmed = true
                };

                await userManager.CreateAsync(adminUser, adminPassword);
                await userManager.AddToRoleAsync(adminUser, "Administrator");
            }
        }
    }
}
