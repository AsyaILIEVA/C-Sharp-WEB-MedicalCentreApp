using MedicalCentreApp.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MedicalCentreApp.Data.Configuration
{
    public class DoctorEntityTypeConfiguration : IEntityTypeConfiguration<Doctor>
    {
        private readonly Doctor[] Doctors =
            {
            new Doctor
            {
                Id = 1,
                FullName = "Dr. Ivan Petrov",
                Specialty = "General Practitioner"
            },
            new Doctor
            {
                Id = 2,
                FullName = "Dr. Maria Ivanova",
                Specialty = "Cardiologist"
            },
            new Doctor
            {
                Id = 3,
                FullName = "Dr. Georgi Dimitrov",
                Specialty = "Dermatologist"
            },
            new Doctor
            {
                Id = 4,
                FullName = "Dr. Elena Stoyanova",
                Specialty = "Pediatrician"
            },
            new Doctor
            {
                Id = 5,
                FullName = "Dr. Nikolay Hristov",
                Specialty = "Neurologist"
            }
        };
        public void Configure(EntityTypeBuilder<Doctor> entity)
        {
            entity.HasData(Doctors);
        }
    }
}
