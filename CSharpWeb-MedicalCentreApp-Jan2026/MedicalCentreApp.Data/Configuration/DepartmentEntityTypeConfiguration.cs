using MedicalCentreApp.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MedicalCentreApp.Data.Configuration
{
    public class DepartmentEntityTypeConfiguration : IEntityTypeConfiguration<Department>
    {
        public void Configure(EntityTypeBuilder<Department> entity)
        {
            entity.HasData(
                new Department
                {
                    Id = 1,
                    Name = "General Medicine",
                    Description = "Primary healthcare services"
                },
                new Department
                {
                    Id = 2,
                    Name = "Cardiology",
                    Description = "Heart related diagnostics and treatment"
                },
                new Department
                {
                    Id = 3,
                    Name = "Dermatology",
                    Description = "Skin conditions and treatment"
                },
                new Department
                {
                    Id = 4,
                    Name = "Pediatrics",
                    Description = "Child healthcare"
                },
                new Department
                {
                    Id = 5,
                    Name = "Neurology",
                    Description = "Nervous system disorders"
                }
            );
        }
    }
}