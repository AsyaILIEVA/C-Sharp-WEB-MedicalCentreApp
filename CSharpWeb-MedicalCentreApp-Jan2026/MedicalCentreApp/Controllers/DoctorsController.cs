using MedicalCentreApp.Services.Core;
using MedicalCentreApp.Services.Core.Interfaces;
using MedicalCentreApp.ViewModels.Doctors;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MedicalCentreApp.Controllers
{
    [Authorize]
    public class DoctorsController : Controller
    {
        private readonly IDoctorService doctorService;

        public DoctorsController(IDoctorService doctorService)
        {
            this.doctorService = doctorService;
        }


        [Authorize(Roles = "Doctor,Patient,Administrator")]
        [HttpGet]
        public async Task<IActionResult> Index(string? specialty, int pageNumber = 1, int pageSize = 10)
        {
            var pagedDoctors = await doctorService
                .GetDoctorsWithPaginationAsync(specialty, pageNumber, pageSize);

            ViewData["CurrentFilter"] = specialty;

            return View(pagedDoctors);
        }

        [Authorize(Roles = "Administrator")]
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [Authorize(Roles = "Administrator")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateDoctorInputModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            await doctorService.CreateAsync(model);

            return RedirectToAction(nameof(Index));
        }

        [Authorize(Roles = "Administrator")]
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var model = await doctorService.GetForEditAsync(id);

            if (model == null)
                return NotFound();

            return View(model);
        }

        [Authorize(Roles = "Administrator")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(EditDoctorInputModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var success = await doctorService.UpdateAsync(model);

            if (!success)
                return NotFound();

            return RedirectToAction(nameof(Index));
        }

        [Authorize(Roles = "Administrator")]
        [HttpGet]

        public async Task<IActionResult> Delete(int id)
        {
            var doctor = await doctorService.GetForDeleteAsync(id);

            if (doctor == null)
                return NotFound();

            return View(doctor);
        }

        [Authorize(Roles = "Administrator")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var success = await doctorService.DeleteAsync(id);

            if (!success)
                return BadRequest("Doctor has appointments and cannot be deleted.");

            return RedirectToAction(nameof(Index));
        }

        [Authorize(Roles = "Doctor,Patient,Administrator")]
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
