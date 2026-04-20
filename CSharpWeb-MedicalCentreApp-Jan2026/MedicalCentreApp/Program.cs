using MedicalCentreApp.Extensions;


namespace MedicalCentreApp
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
                        
            builder.Services.AddDatabaseDeveloperPageExceptionFilter();
                        
            builder.Services.AddApplicationInfrastructure(builder.Configuration);
            builder.Services.AddApplicationServices();

            builder.Services.AddControllersWithViews();
            builder.Services.AddRazorPages();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            await app.SeedApplicationAsync();

            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/ServerError");                
            }

            app.UseStatusCodePagesWithReExecute("/Home/HandleError", "?statusCode={0}");

            if (app.Environment.IsDevelopment())
            {
                app.UseMigrationsEndPoint();
            }
            else
            {  
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllerRoute(
                name: "areas",
                pattern: "{area:exists}/{controller=Dashboard}/{action=Index}/{id?}");

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");
                        
            app.MapRazorPages();           

            app.Run();
        }        
    }
}
