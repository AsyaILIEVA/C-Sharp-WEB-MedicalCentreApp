using MedicalCentreApp.Data;
using MedicalCentreApp.Data.Models;
using MedicalCentreApp.Services.Core;
using MedicalCentreApp.Services.Core.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace MedicalCentreApp
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            var connectionString = builder.Configuration.GetConnectionString("DevConnection");

            builder.Services.AddDbContext<MedicalCentreAppDbContext>(options =>
                options.UseSqlServer(connectionString));
           
            builder.Services.AddDatabaseDeveloperPageExceptionFilter();                       

            builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
                {
                    options.SignIn.RequireConfirmedAccount = false;

                })
                .AddEntityFrameworkStores<MedicalCentreAppDbContext>()
                .AddDefaultTokenProviders();

            builder.Services.AddControllersWithViews();
            builder.Services.AddRazorPages();

            builder.Services.AddScoped<IDoctorService, DoctorService>();

            builder.Services.AddScoped<IPatientService, PatientService>();

            builder.Services.AddScoped<IAppointmentService, AppointmentService>();

            var app = builder.Build();

            
            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseMigrationsEndPoint();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");
            app.MapRazorPages();

            app.Run();
        }
    }
}
