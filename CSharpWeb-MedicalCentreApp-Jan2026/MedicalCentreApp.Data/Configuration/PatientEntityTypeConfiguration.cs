using MedicalCentreApp.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MedicalCentreApp.Data.Configuration
{
    public class PatientEntityTypeConfiguration : IEntityTypeConfiguration<Patient>
    {
        private readonly Patient[] Patients =
        {
            new Patient
            {
                Id = 1,
                FirstName = "Alexander",
                MiddleName = "Petrov",
                LastName = "Krastev",
                EGN = "8902141234",
                DateOfBirth = new DateTime(1989, 2, 14),
                PhoneNumber = "0897123456",
                Email = "a.krastev@example.com"
            },
            new Patient
            {
                Id = 2,
                FirstName = "Milena",
                MiddleName = "Georgieva",
                LastName = "Zlateva",
                EGN = "9406232345",
                DateOfBirth = new DateTime(1994, 6, 23),
                PhoneNumber = "0897234567",
                Email = "zlateva548@example.com"
            },
            new Patient
            {
                Id = 3,
                FirstName = "Todor",
                MiddleName = "Angelov",
                LastName = "Angelov",
                EGN = "8609113456",
                DateOfBirth = new DateTime(1986, 9, 11),
                PhoneNumber = "0897345678",
                Email = "angelov.todor@example.com"
            },
            new Patient
            {
                Id = 4,
                FirstName = "Radostina",
                MiddleName = "Radeva",
                LastName = "Ilieva",
                EGN = "9704024567",
                DateOfBirth = new DateTime(1997, 4, 2),
                PhoneNumber = "0897456789",
                Email = "radostina97@example.com"
            },
            new Patient
            {
                Id = 5,
                FirstName = "Borislav",
                MiddleName = "Parvanov",
                LastName = "Naydenov",
                EGN = "8807195678",
                DateOfBirth = new DateTime(1988, 7, 19),
                PhoneNumber = "0897567890",
                Email = "parvanov_bn@example.com"
            },
            new Patient
            {
                Id = 6,
                FirstName = "Yana",
                MiddleName = "Dimitrova",
                LastName = "Kostova",
                EGN = "9501286789",
                DateOfBirth = new DateTime(1995, 1, 28),
                PhoneNumber = "0897678901",
                Email = "kostova@example.com"
            },
            new Patient
            {
                Id = 7,
                FirstName = "Plamen",
                MiddleName = "Atanasov",
                LastName = "Vasilev",
                EGN = "8706107890",
                DateOfBirth = new DateTime(1987, 6, 10),
                PhoneNumber = "0897789012",
                Email = "p_vasilev@example.com"

            },
            new Patient
            {
                Id = 8,
                FirstName = "Kristina",
                MiddleName = "Petrova",
                LastName = "Marinova",
                EGN = "9309248901",
                DateOfBirth = new DateTime(1993, 9, 24),
                PhoneNumber = "0897890123",
                Email = "k.marinova@example.com"
            },
            new Patient
            {
                Id = 9,
                FirstName = "Stanislav",
                MiddleName = "Tomov",
                LastName = "Rusev",
                EGN = "9103179012",
                DateOfBirth = new DateTime(1991, 3, 17),
                PhoneNumber = "0897901234",
                Email = "rusev@example.com"
            },
            new Patient
            {
                Id = 10,
                FirstName = "Gabriela",
                MiddleName = "Danova",
                LastName = "Tsvetkova",
                EGN = "9812050123",
                DateOfBirth = new DateTime(1998, 12, 5),
                PhoneNumber = "0897012345",
                Email = "gabi123@example.com"
            }
        };
        public void Configure(EntityTypeBuilder<Patient> entity)
        {
            entity.HasData(Patients);
        }
    }
}
