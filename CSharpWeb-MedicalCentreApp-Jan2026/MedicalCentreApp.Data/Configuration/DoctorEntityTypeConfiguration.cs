using MedicalCentreApp.Data.Models;
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
                Specialty = "General Practitioner",
                DepartmentId = 1
            },
            new Doctor
            {
                Id = 2,
                FullName = "Dr. Maria Ivanova",
                Specialty = "Cardiologist",
                DepartmentId = 2
            },
            new Doctor
            {
                Id = 3,
                FullName = "Dr. Georgi Dimitrov",
                Specialty = "Dermatologist",
                DepartmentId = 3
            },
            new Doctor
            {
                Id = 4,
                FullName = "Dr. Elena Stoyanova",
                Specialty = "Pediatrician",
                DepartmentId = 4
            },
            new Doctor
            {
                Id = 5,
                FullName = "Dr. Nikolay Hristov",
                Specialty = "Neurologist",
                DepartmentId = 5
            }            
        };

        public void Configure(EntityTypeBuilder<Doctor> entity)
        {
            entity.HasData(Doctors);
        }
    }
}
