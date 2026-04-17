using MedicalCentreApp.Data.Models;
using MedicalCentreApp.Data.Repositories.Interfaces;
using MedicalCentreApp.GCommon;
using MedicalCentreApp.Services.Core.Interfaces;
using MedicalCentreApp.ViewModels.Doctors;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace MedicalCentreApp.Services.Core
{
    public class DoctorService : IDoctorService
    {
        private readonly IDoctorRepository doctorRepository;

        public DoctorService(IDoctorRepository doctorRepository)
        {
            this.doctorRepository = doctorRepository;
        }

        public async Task<PagedList<DoctorListViewModel>> GetDoctorsWithPaginationAsync(
                string? specialty, int pageNumber, int pageSize)
        {
            var query = doctorRepository
                .AllAsNoTracking();

            if (!string.IsNullOrWhiteSpace(specialty))
            {
                query = query.Where(d => d.Specialty.Contains(specialty));
            }

            var projectedQuery = query
                .OrderBy(d => d.FullName)
                .Select(d => new DoctorListViewModel
                {
                    Id = d.Id,
                    FullName = d.FullName,
                    Specialty = d.Specialty,
                    ImageUrl = d.ImageUrl
                });

            return await PagedList<DoctorListViewModel>
                .CreateAsync(projectedQuery, pageNumber, pageSize);
        }

        public async Task<IEnumerable<DoctorListViewModel>> GetAllAsync(string? specialty)
        {
            var query = doctorRepository.AllAsNoTracking();

            if (!string.IsNullOrWhiteSpace(specialty))
            {
                query = query.Where(d => d.Specialty.Contains(specialty));
            }

            IEnumerable<DoctorListViewModel> doctors = await query
                .OrderBy(d => d.FullName)
                .Select(d => new DoctorListViewModel
                {
                    Id = d.Id,
                    FullName = d.FullName,
                    Specialty = d.Specialty,
                    ImageUrl = d.ImageUrl
                })
                .ToListAsync();

            return doctors;
        }

        public async Task CreateAsync(CreateDoctorInputModel model)
        {
            string? imageUrl = await SaveImageAsync(model.Image);

            bool exists = await doctorRepository
                .AllAsNoTracking()
                .AnyAsync(d =>
                    d.FullName == model.FullName &&
                    d.Specialty == model.Specialty &&
                    d.DepartmentId == model.DepartmentId);

            if (exists)
            {
                throw new InvalidOperationException("Doctor already exists.");
            }

            Doctor doctor = new Doctor
            {
                FullName = model.FullName,
                Specialty = model.Specialty,
                DepartmentId = model.DepartmentId,
                ImageUrl = imageUrl
            };

            await doctorRepository.AddAsync(doctor);
            await doctorRepository.SaveChangesAsync();
        }

        public async Task<EditDoctorInputModel?> GetForEditAsync(int id)
        {
            var doctor = await doctorRepository.GetByIdAsync(id);

            if (doctor == null) return null;

            EditDoctorInputModel editModel = new EditDoctorInputModel
            {
                Id = doctor.Id,
                FullName = doctor.FullName,
                Specialty = doctor.Specialty,
                DepartmentId = doctor.DepartmentId,
                ExistingImageUrl = doctor.ImageUrl
            };

            return editModel;
        }

        public async Task<bool> UpdateAsync(EditDoctorInputModel model)
        {
            var doctor = await doctorRepository.GetByIdAsync(model.Id);

            if (doctor == null) return false;

            doctor.FullName = model.FullName;
            doctor.Specialty = model.Specialty;
            doctor.DepartmentId = model.DepartmentId;

            if (model.Image != null)
            {
                doctor.ImageUrl = await SaveImageAsync(model.Image);
            }

            doctorRepository.Update(doctor);
            await doctorRepository.SaveChangesAsync();

            return true;
        }

        public async Task<DoctorDetailsViewModel?> GetForDeleteAsync(int id)
        {
            DoctorDetailsViewModel? deleteViewModel = await doctorRepository
                .AllAsNoTracking()
                .Where(d => d.Id == id)
                .Select(d => new DoctorDetailsViewModel
                {
                    Id = d.Id,
                    FullName = d.FullName,
                    Specialty = d.Specialty,
                    ImageUrl = d.ImageUrl,
                    DepartmentName = d.Department.Name
                })
                .FirstOrDefaultAsync();

            return deleteViewModel;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var doctor = await doctorRepository.GetDoctorWithAppointmentsAsync(id);

            if (doctor == null) return false;

            if (doctor.Appointments.Any()) return false;

            doctorRepository.Delete(doctor);
            await doctorRepository.SaveChangesAsync();

            return true;
        }

        public async Task<DoctorDetailsViewModel?> GetDetailsAsync(int id)
        {
            DoctorDetailsViewModel? detailsViewModel = await doctorRepository
                .AllAsNoTracking()
                .Where(d => d.Id == id)
                .Select(d => new DoctorDetailsViewModel
                {
                    Id = d.Id,
                    FullName = d.FullName,
                    Specialty = d.Specialty,
                    ImageUrl = d.ImageUrl,
                    DepartmentName = d.Department.Name
                })
                .FirstOrDefaultAsync();

            return detailsViewModel;
        }

        private async Task<string?> SaveImageAsync(IFormFile? image)
        {
            if (image == null) return null;

            var allowedExtensions = new[] { ".jpg", ".jpeg", ".png" };
            var extension = Path.GetExtension(image.FileName).ToLower();

            if (!allowedExtensions.Contains(extension)) return null;

            if (image.Length > ViewModelValidation.DoctorViewModels.MaxImageSizeInBytes) return null;

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