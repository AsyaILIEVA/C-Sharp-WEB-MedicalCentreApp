using MedicalCentreApp.Models;
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
                LastName = "Krastev",
                EGN = "8902141234",
                DateOfBirth = new DateTime(1989, 2, 14),
                PhoneNumber = "0897123456"
            },
            new Patient
            {
                Id = 2,
                FirstName = "Milena",
                LastName = "Zlateva",
                EGN = "9406232345",
                DateOfBirth = new DateTime(1994, 6, 23),
                PhoneNumber = "0897234567"
            },
            new Patient
            {
                Id = 3,
                FirstName = "Todor",
                LastName = "Angelov",
                EGN = "8609113456",
                DateOfBirth = new DateTime(1986, 9, 11),
                PhoneNumber = "0897345678"
            },
            new Patient
            {
                Id = 4,
                FirstName = "Radostina",
                LastName = "Ilieva",
                EGN = "9704024567",
                DateOfBirth = new DateTime(1997, 4, 2),
                PhoneNumber = "0897456789"
            },
            new Patient
            {
                Id = 5,
                FirstName = "Borislav",
                LastName = "Naydenov",
                EGN = "8807195678",
                DateOfBirth = new DateTime(1988, 7, 19),
                PhoneNumber = "0897567890"
            },
            new Patient
            {
                Id = 6,
                FirstName = "Yana",
                LastName = "Kostova",
                EGN = "9501286789",
                DateOfBirth = new DateTime(1995, 1, 28),
                PhoneNumber = "0897678901"
            },
            new Patient
            {
                Id = 7,
                FirstName = "Plamen",
                LastName = "Vasilev",
                EGN = "8706107890",
                DateOfBirth = new DateTime(1987, 6, 10),
                PhoneNumber = "0897789012"
            },
            new Patient
            {
                Id = 8,
                FirstName = "Kristina",
                LastName = "Marinova",
                EGN = "9309248901",
                DateOfBirth = new DateTime(1993, 9, 24),
                PhoneNumber = "0897890123"
            },
            new Patient
            {
                Id = 9,
                FirstName = "Stanislav",
                LastName = "Rusev",
                EGN = "9103179012",
                DateOfBirth = new DateTime(1991, 3, 17),
                PhoneNumber = "0897901234"
            },
            new Patient
            {
                Id = 10,
                FirstName = "Gabriela",
                LastName = "Tsvetkova",
                EGN = "9812050123",
                DateOfBirth = new DateTime(1998, 12, 5),
                PhoneNumber = "0897012345"
            }
        };
        public void Configure(EntityTypeBuilder<Patient> entity)
        {
            entity.HasData(Patients);
        }
    }
}
