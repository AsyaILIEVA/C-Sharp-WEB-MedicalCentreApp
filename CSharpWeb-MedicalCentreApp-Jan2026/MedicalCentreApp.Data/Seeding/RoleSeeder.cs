using MedicalCentreApp.Data;
using MedicalCentreApp.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

public static class RoleSeeder
{  
    public static async Task SeedAsync(IServiceProvider serviceProvider)
    {
        var dbContext = serviceProvider
            .GetRequiredService<MedicalCentreAppDbContext>();

        var roleManager = serviceProvider
            .GetRequiredService<RoleManager<IdentityRole>>();

        string[] roles = { "Administrator", "User", "Doctor", "Patient" };

        foreach (var role in roles)
        {
            if (!await roleManager.RoleExistsAsync(role))
            {
                await roleManager.CreateAsync(new IdentityRole(role));
            }
        }

        var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();

        var adminEmail = "admin@medical.com";

        var admin = await userManager.FindByEmailAsync(adminEmail);

        if (admin == null)
        {
            var newAdmin = new ApplicationUser
            {
                UserName = adminEmail,
                Email = adminEmail
            };

            await userManager.CreateAsync(newAdmin, "Admin123!");

            await userManager.AddToRoleAsync(newAdmin, "Administrator");
        }

        await SeedDepartments(dbContext);
        await SeedPatients(userManager, dbContext);
        await SeedDoctors(userManager, dbContext);
    }

    private static async Task SeedPatients(
    UserManager<ApplicationUser> userManager,
    MedicalCentreAppDbContext dbContext)
    {        
        var patients = new List<(string Email, string Password, string FirstName, string MiddleName, string LastName)>
    {
        ("patient1@medical.com", "Test123!", "Ivan", "Petrov", "Petrov"),
        ("patient2@medical.com", "Test123!", "Maria", "Dimitrova", "Ivanova"),
        ("patient3@medical.com", "Test123!", "Georgi", "Ivanov", "Georgiev"),
        ("patient4@medical.com", "Test123!", "Elena", "Petrova", "Dimitrova"),
        ("patient5@medical.com", "Test123!", "Nikolay", "Georgiev", "Kolev"),
        ("patient6@medical.com", "Test123!", "Petya", "Marinova", "Stoyanova"),
        ("patient7@medical.com", "Test123!", "Stoyan", "Radev", "Ivanov"),
        ("patient8@medical.com", "Test123!", "Gergana", "Petrova", "Petrova"),
        ("patient9@medical.com", "Test123!", "Todor", "Georgiev", "Angelov"),
        ("patient10@medical.com", "Test123!", "Viktoria", "Ivanova", "Nikolova"),
        ("patient11@medical.com", "Test123!", "Dimitar", "Stoyanov", "Dimitrov"),
        ("patient12@medical.com", "Test123!", "Radoslav", "Peev", "Radev"),
        ("patient13@medical.com", "Test123!", "Ivaylo", "Todorov", "Petkov"),
        ("patient14@medical.com", "Test123!", "Kalina", "Pavlova", "Georgieva"),
        ("patient15@medical.com", "Test123!", "Desislava", "Todorova", "Ivanova"),
        ("patient16@medical.com", "Test123!", "Hristo", "Ivanov", "Hristov"),
        ("patient17@medical.com", "Test123!", "Teodora", "Georgieva", "Todorova"),
        ("patient18@medical.com", "Test123!", "Borislav", "Parvanov", "Borisov"),
        ("patient19@medical.com", "Test123!", "Simona", "Krasteva", "Simeonova"),
        ("patient20@medical.com", "Test123!", "Valentin", "Radev", "Valentinov")
    };

        int counter = 0;

        foreach (var p in patients)
        {
            var existingUser = await userManager.FindByEmailAsync(p.Email);
            if (existingUser != null)
                continue;

            var user = new ApplicationUser
            {
                UserName = p.Email,
                Email = p.Email
            };

            var result = await userManager.CreateAsync(user, p.Password);

            if (!result.Succeeded)
            {
                var errors = string.Join(", ", result.Errors.Select(e => e.Description));
                throw new Exception(errors);
            }

            await userManager.AddToRoleAsync(user, "Patient");

            dbContext.Patients.Add(new Patient
            {
                FirstName = p.FirstName,
                MiddleName = p.MiddleName,
                LastName = p.LastName,
                Email = p.Email,
                UserId = user.Id,
                EGN = $"900101{(1000 + counter)}",
                DateOfBirth = DateTime.Now.AddYears(-30),
                PhoneNumber = "0888123456",
                Address = "Sofia"
            });

            counter++;
        }

        await dbContext.SaveChangesAsync();
    }

    private static async Task SeedDepartments(MedicalCentreAppDbContext dbContext)
    {
        if (dbContext.Departments.Any())
            return;

        dbContext.Departments.AddRange(
            new Department { Name = "Cardiology" },
            new Department { Name = "Dermatology" },
            new Department { Name = "Neurology" },
            new Department { Name = "Pediatrics" },
            new Department { Name = "Orthopedics" }
        );

        await dbContext.SaveChangesAsync();
    }

    private static async Task SeedDoctors(
    UserManager<ApplicationUser> userManager,
    MedicalCentreAppDbContext dbContext)
    {        
        var departments = dbContext.Departments.ToList();

        var doctors = new List<(string Email, string Password, string Name, string Specialty)>
    {
        ("doc1@medical.com", "Test123!", "Dr. Ivan Petrov", "Cardiology"),
        ("doc2@medical.com", "Test123!", "Dr. Maria Ivanova", "Dermatology"),
        ("doc3@medical.com", "Test123!", "Dr. Georgi Georgiev", "Neurology"),
        ("doc4@medical.com", "Test123!", "Dr. Elena Dimitrova", "Pediatrics"),
        ("doc5@medical.com", "Test123!", "Dr. Nikolay Kolev", "Orthopedics"),
        ("doc6@medical.com", "Test123!", "Dr. Boyan Pavlov", "Cardiology"),
        ("doc7@medical.com", "Test123!", "Dr. Marina Ilieva", "Dermatology"),
        ("doc8@medical.com", "Test123!", "Dr. Georgi Stoyanov", "Neurology"),
        ("doc9@medical.com", "Test123!", "Dr. Inanka Dimova", "Pediatrics"),
        ("doc10@medical.com", "Test123!", "Dr. Nikola Ivanov", "Orthopedics"),
        ("doc11@medical.com", "Test123!", "Dr. Polina Grigorova", "Pediatrics"),
        ("doc12@medical.com", "Test123!", "Dr. Stoyan Zahariev", "Orthopedics")
    };

        foreach (var d in doctors)
        {
            var existingUser = await userManager.FindByEmailAsync(d.Email);
            if (existingUser != null)
                continue;

            var user = new ApplicationUser
            {
                UserName = d.Email,
                Email = d.Email
            };

            var result = await userManager.CreateAsync(user, d.Password);

            if (!result.Succeeded)
            {
                var errors = string.Join(", ", result.Errors.Select(e => e.Description));
                throw new Exception(errors);
            }

            await userManager.AddToRoleAsync(user, "Doctor");

            bool doctorExists = dbContext.Doctors.Any(x => x.UserId == user.Id);
            if (doctorExists)
                continue;

            var department = departments
            .FirstOrDefault(x => x.Name == d.Specialty)
            ?? departments.First();

            dbContext.Doctors.Add(new Doctor
            {
                FullName = d.Name,
                Specialty = d.Specialty,
                UserId = user.Id,
                DepartmentId = department.Id
            });
        }

        await dbContext.SaveChangesAsync();
    }
}