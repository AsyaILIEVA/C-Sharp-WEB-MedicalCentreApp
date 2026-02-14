using MedicalCentreApp.Services.Core.Interfaces;
using MedicalCentreApp.ViewModels.Doctors;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MedicalCentreApp.Controllers
{
    public class DoctorsController : Controller
    {
        private readonly IDoctorService doctorService;

        public DoctorsController(IDoctorService doctorService)
        {
            this.doctorService = doctorService;
        }

        [HttpGet]
        public async Task<IActionResult> Index(string? specialty)
        {
            var doctors = await doctorService.GetAllAsync(specialty);

            ViewData["CurrentFilter"] = specialty;

            return View(doctors);
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create(CreateDoctorInputModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            await doctorService.CreateAsync(model);

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var model = await doctorService.GetForEditAsync(id);

            if (model == null)
                return NotFound();

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditDoctorInputModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var success = await doctorService.UpdateAsync(model);

            if (!success)
                return NotFound();

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var doctor = await doctorService.GetForDeleteAsync(id);

            if (doctor == null)
                return NotFound();

            return View(doctor);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var success = await doctorService.DeleteAsync(id);

            if (!success)
                return BadRequest("Doctor has appointments and cannot be deleted.");

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var doctor = await doctorService.GetDetailsAsync(id);

            if (doctor == null)
                return NotFound();

            return View(doctor);
        }
    }
}
