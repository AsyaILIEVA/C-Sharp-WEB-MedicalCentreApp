using MedicalCentreApp.Services.Core.Interfaces;
using MedicalCentreApp.ViewModels.Patients;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System.Security.Claims;

namespace MedicalCentreApp.Controllers
{
    [Authorize]
    public class PatientsController : Controller
    {
        private readonly IPatientService patientService;

        public PatientsController(IPatientService patientService)
        {
            this.patientService = patientService;
        }

        [Authorize(Roles = "Doctor,Administrator")]
        public async Task<IActionResult> Index(int page = 1, int pageSize = 10)
        {
            var result = await patientService.GetPagedAsync(page, pageSize);

            return View(result);
        }

        public async Task<IActionResult> Details(int id)
        {
            var patient = await patientService.GetDetailsAsync(id);
            if (patient == null)
                return NotFound();

            if (!await IsOwnerPatient(id))
                return Forbid();

            return View(patient);
        }
        
        [Authorize(Roles = "Patient")]
        public async Task<IActionResult> MyDetails()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var patient = await patientService.GetDetailsByUserIdAsync(userId);

            if (patient == null)
                return NotFound();

            return View("Details", patient);
        }

        [Authorize(Roles = "Patient")]
        public async Task<IActionResult> CompleteProfile()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var model = await patientService.GetProfileByUserIdAsync(userId);

            if (model == null)
                return NotFound();

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Patient")]
        public async Task<IActionResult> CompleteProfile(PatientProfileViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var result = await patientService.UpdateProfileAsync(userId, model);

            if (!result)
                return BadRequest();

            return RedirectToAction("MyDetails");
        }

        [Authorize(Roles = "Patient")]
        public async Task<IActionResult> Profile()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var patient = await patientService.GetDetailsByUserIdAsync(userId);

            if (patient == null)
                return NotFound();

            return View("Details", patient);
        }

        [Authorize(Roles = "Administrator")]
        [HttpGet]
        public IActionResult Create()
        {
            return View(new CreateEditPatientViewModel
            {
                DateOfBirth = DateTime.Today
            });
        }

        [Authorize(Roles = "Administrator")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateEditPatientViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            try
            {
                await patientService.CreateAsync(model);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View(model);
            }

            return RedirectToAction(nameof(Index));
        }

        [Authorize(Roles = "Doctor,Patient,Administrator")]
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var model = await patientService.GetForEditAsync(id);
            if (model == null)
                return NotFound();

            return View(model);
        }

        [Authorize(Roles = "Doctor,Patient,Administrator")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, CreateEditPatientViewModel model)
        {
            if (id != model.Id)
                return BadRequest();
            
            if (!await CanEditPatient(id))
                return Forbid();

            if (!ModelState.IsValid)
                return View(model);

            var success = await patientService.UpdateAsync(id, model);
            if (!success)
                return BadRequest();

            return RedirectToAction(nameof(Details), new { id });
        }

        [Authorize(Roles = "Doctor,Administrator")]
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var patient = await patientService.GetForDeleteAsync(id);
            if (patient == null)
                return NotFound();

            return View(patient);
        }

        [Authorize(Roles = "Doctor,Administrator")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var success = await patientService.DeleteAsync(id);
            if (!success)
                return NotFound();

            return RedirectToAction(nameof(Index));
        }

        [Authorize(Roles = "Doctor,Patient,Administrator")]
        public async Task<IActionResult> MedicalRecords(int id)
        {
            if (!await IsOwnerPatient(id))
                return Forbid();

            var records = await patientService.GetMedicalRecordsAsync(id);
            ViewBag.PatientId = id;

            return View(records);
        }

        private async Task<bool> IsOwnerPatient(int patientId)
        {
            if (User.IsInRole("Doctor") || User.IsInRole("Administrator"))
                return true;

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var patient = await patientService.GetDetailsByUserIdAsync(userId);

            return patient != null && patient.Id == patientId;
        }

        private async Task<bool> CanEditPatient(int patientId)
        {
            if (User.IsInRole("Doctor") || User.IsInRole("Administrator"))
                return true;

            if (User.IsInRole("Patient"))
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                var patient = await patientService.GetDetailsByUserIdAsync(userId);

                return patient != null && patient.Id == patientId;
            }

            return false;
        }
    }
}
