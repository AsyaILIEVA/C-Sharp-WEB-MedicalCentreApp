using MedicalCentreApp.Common;
using MedicalCentreApp.Data;
using MedicalCentreApp.Models;
using MedicalCentreApp.ViewModels.Doctors;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MedicalCentreApp.Controllers
{
    public class DoctorsController : Controller
    {
        private readonly MedicalCentreAppDbContext dbContext;

        public DoctorsController(MedicalCentreAppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
                
        [HttpGet]
        public async Task<IActionResult> Index(string? specialty)
        {            
            var doctorsQuery = dbContext
                .Doctors
                .AsNoTracking();
            
            if (!string.IsNullOrWhiteSpace(specialty))
            {
                doctorsQuery = doctorsQuery
                    .Where(d => d.Specialty
                    .Contains(specialty));
            }
            
            var doctors = await doctorsQuery
                .OrderBy(d => d.FullName)
                .ToListAsync();
            
            ViewData["CurrentFilter"] = specialty;

            return View(doctors);
        }

        [HttpGet]
        //[Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        //[Authorize(Roles = "Admin")]
        public IActionResult Create(CreateDoctorInputModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            string? imageUrl = null;

            if (model.Image != null)
            {
                
                var allowedExtensions = new[] { ".jpg", ".jpeg", ".png" };
                var extension = Path.GetExtension(model.Image.FileName).ToLower();

                if (!allowedExtensions.Contains(extension))
                {
                    ModelState.AddModelError("Image", "Only JPG and PNG images are allowed.");
                    return View(model);
                }

                if (model.Image.Length > EntityValidation.MaxImageSizeInBytes)
                {
                    ModelState.AddModelError("Image", "Image size must be less than 2 MB.");
                    return View(model);
                }
                
                string uploadsFolder = Path.Combine(
                    Directory.GetCurrentDirectory(),
                    "wwwroot/images/doctors");

                Directory.CreateDirectory(uploadsFolder);

                string fileName = Guid.NewGuid() + extension;
                string filePath = Path.Combine(uploadsFolder, fileName);

                using var stream = new FileStream(filePath, FileMode.Create);
                model.Image.CopyTo(stream);

                imageUrl = "/images/doctors/" + fileName;
            }

            Doctor doctor = new Doctor
            {
                FullName = model.FullName,
                Specialty = model.Specialty,
                ImageUrl = imageUrl
            };

            dbContext.Doctors.Add(doctor);
            dbContext.SaveChanges();

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        //[Authorize(Roles = "Admin")]
        public IActionResult Edit(int id)
        {
            var doctor = dbContext.Doctors.Find(id);

            if (doctor == null)
            {
                return NotFound();
            }

            var model = new EditDoctorInputModel
            {
                Id = doctor.Id,
                FullName = doctor.FullName,
                Specialty = doctor.Specialty,
                ExistingImageUrl = doctor.ImageUrl
            };

            return View(model);
        }

        [HttpPost]
        //[Authorize(Roles = "Admin")]
        public IActionResult Edit(EditDoctorInputModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var doctor = dbContext.Doctors.Find(model.Id);

            if (doctor == null)
            {
                return NotFound();
            }

            doctor.FullName = model.FullName;
            doctor.Specialty = model.Specialty;

            if (model.Image != null)
            {
                
                var allowedExtensions = new[] { ".jpg", ".jpeg", ".png" };
                var extension = Path.GetExtension(model.Image.FileName).ToLower();

                if (!allowedExtensions.Contains(extension))
                {
                    ModelState.AddModelError("Image", "Only JPG and PNG images are allowed.");
                    model.ExistingImageUrl = doctor.ImageUrl;
                    return View(model);
                }

                if (model.Image.Length > EntityValidation.MaxImageSizeInBytes)
                {
                    ModelState.AddModelError(
                        "Image",
                        $"Image size must be less than {EntityValidation.MaxImageSizeInBytes / (1024 * 1024)} MB.");

                    model.ExistingImageUrl = doctor.ImageUrl;
                    return View(model);
                }
                
                string uploadsFolder = Path.Combine(
                    Directory.GetCurrentDirectory(),
                    "wwwroot/images/doctors");

                Directory.CreateDirectory(uploadsFolder);

                string fileName = Guid.NewGuid() + extension;
                string filePath = Path.Combine(uploadsFolder, fileName);

                using var stream = new FileStream(filePath, FileMode.Create);
                model.Image.CopyTo(stream);                

                doctor.ImageUrl = "/images/doctors/" + fileName;
            }

            dbContext.SaveChanges();

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        //[Authorize(Roles = "Admin")]
        public IActionResult Delete(int id)
        {
            var doctor = dbContext
                .Doctors
                .AsNoTracking()
                .FirstOrDefault(d => d.Id == id);

            if (doctor == null)
            {
                return NotFound();
            }

            return View(doctor);
        }

        [HttpPost]
        //[Authorize(Roles = "Admin")]
        public IActionResult DeleteConfirmed(int id)
        {
            var doctor = dbContext
                .Doctors
                .Include(d => d.Appointments)
                .FirstOrDefault(d => d.Id == id);

            if (doctor == null)
            {
                return NotFound();
            }

            if (doctor.Appointments.Any())
            {                
                return BadRequest("Doctor has appointments and cannot be deleted.");
            }

            dbContext.Doctors.Remove(doctor);
            dbContext.SaveChanges();

            return RedirectToAction(nameof(Index));
        }
                

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var doctor = await dbContext.Doctors
                .AsNoTracking()
                .Where(d => d.Id == id)
                .Select(d => new DoctorDetailsViewModel
                {
                    Id = d.Id,
                    FullName = d.FullName,
                    Specialty = d.Specialty,
                    ImageUrl = d.ImageUrl,
                    //AppointmentsCount = d.Appointments.Count()
                })
                .FirstOrDefaultAsync();

            if (doctor == null)
            {
                return NotFound();
            }

            return View(doctor);
        }
    }
}
