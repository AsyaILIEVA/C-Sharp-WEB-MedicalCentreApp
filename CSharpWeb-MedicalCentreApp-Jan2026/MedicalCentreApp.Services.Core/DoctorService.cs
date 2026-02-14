
using MedicalCentreApp.Data;
using MedicalCentreApp.Data.Models;
using MedicalCentreApp.GCommon;
using MedicalCentreApp.Services.Core.Interfaces;
using MedicalCentreApp.ViewModels.Doctors;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace MedicalCentreApp.Services.Core
{
    public class DoctorService : IDoctorService
    {
        private readonly MedicalCentreAppDbContext dbContext;

        public DoctorService(MedicalCentreAppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<IEnumerable<Doctor>> GetAllAsync(string? specialty)
        {
            var query = dbContext.Doctors.AsNoTracking();

            if (!string.IsNullOrWhiteSpace(specialty))
            {
                query = query
                    .Where(d => d.Specialty.Contains(specialty));
            }

            return await query
                .OrderBy(d => d.FullName)
                .ToListAsync();
        }

        public async Task CreateAsync(CreateDoctorInputModel model)
        {
            string? imageUrl = await SaveImageAsync(model.Image);

            Doctor doctor = new Doctor
            {
                FullName = model.FullName,
                Specialty = model.Specialty,
                ImageUrl = imageUrl
            };

            dbContext.Doctors.Add(doctor);
            await dbContext.SaveChangesAsync();
        }

        public async Task<EditDoctorInputModel?> GetForEditAsync(int id)
        {
            var doctor = await dbContext.Doctors.FindAsync(id);

            if (doctor == null) 
            {
                return null; 
            }

            return new EditDoctorInputModel
            {
                Id = doctor.Id,
                FullName = doctor.FullName,
                Specialty = doctor.Specialty,
                ExistingImageUrl = doctor.ImageUrl
            };
        }

        public async Task<bool> UpdateAsync(EditDoctorInputModel model)
        {
            var doctor = await dbContext.Doctors.FindAsync(model.Id);
            if (doctor == null) return false;

            doctor.FullName = model.FullName;
            doctor.Specialty = model.Specialty;

            if (model.Image != null)
            {
                doctor.ImageUrl = await SaveImageAsync(model.Image);
            }

            await dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<Doctor?> GetForDeleteAsync(int id)
        {
            return await dbContext.Doctors
                .AsNoTracking()
                .FirstOrDefaultAsync(d => d.Id == id);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var doctor = await dbContext.Doctors
                .Include(d => d.Appointments)
                .FirstOrDefaultAsync(d => d.Id == id);

            if (doctor == null) return false;

            if (doctor.Appointments.Any())
                return false;

            dbContext.Doctors.Remove(doctor);
            await dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<DoctorDetailsViewModel?> GetDetailsAsync(int id)
        {
            return await dbContext.Doctors
                .AsNoTracking()
                .Where(d => d.Id == id)
                .Select(d => new DoctorDetailsViewModel
                {
                    Id = d.Id,
                    FullName = d.FullName,
                    Specialty = d.Specialty,
                    ImageUrl = d.ImageUrl
                })
                .FirstOrDefaultAsync();
        }

        private async Task<string?> SaveImageAsync(IFormFile? image)
        {
            if (image == null) return null;

            var allowedExtensions = new[] { ".jpg", ".jpeg", ".png" };
            var extension = Path.GetExtension(image.FileName).ToLower();

            if (!allowedExtensions.Contains(extension))
                //throw new InvalidOperationException("Invalid image format.");
                return null;

            if (image.Length > EntityValidation.MaxImageSizeInBytes)
                //throw new InvalidOperationException("Image too large.");
                return null;

            string uploadsFolder = Path.Combine(
                Directory.GetCurrentDirectory(),
                "wwwroot",
                "images",
                "doctors");

            Directory.CreateDirectory(uploadsFolder);

            string fileName = Guid.NewGuid() + extension;
            string filePath = Path.Combine(uploadsFolder, fileName);

            using var stream = new FileStream(filePath, FileMode.Create);
            await image.CopyToAsync(stream);

            return "/images/doctors/" + fileName;
        }
    }
}
