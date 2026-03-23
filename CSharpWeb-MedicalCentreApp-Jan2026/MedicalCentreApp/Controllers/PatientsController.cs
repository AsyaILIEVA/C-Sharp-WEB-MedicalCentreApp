using MedicalCentreApp.Services.Core.Interfaces;
using MedicalCentreApp.ViewModels.Patients;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System.Security.Claims;

namespace MedicalCentreApp.Controllers
{
    public class PatientsController : Controller
    {
        private readonly IPatientService patientService;

        public PatientsController(IPatientService patientService)
        {
            this.patientService = patientService;
        }

        [Authorize(Roles = "Doctor")]
        public async Task<IActionResult> Index()
        {
            var patients = await patientService.GetAllAsync();
            return View(patients);
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

        [Authorize(Roles = "Doctor")]
        [HttpGet]
        public IActionResult Create()
        {
            return View(new CreateEditPatientViewModel
            {
                DateOfBirth = DateTime.Today
            });
        }

        [Authorize(Roles = "Doctor")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateEditPatientViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            await patientService.CreateAsync(model);
            return RedirectToAction(nameof(Index));
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var model = await patientService.GetForEditAsync(id);
            if (model == null)
                return NotFound();

            return View(model);
        }

        [Authorize]
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

        [Authorize(Roles = "Doctor")]
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var patient = await patientService.GetForDeleteAsync(id);
            if (patient == null)
                return NotFound();

            return View(patient);
        }

        [Authorize(Roles = "Doctor")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var success = await patientService.DeleteAsync(id);
            if (!success)
                return NotFound();

            return RedirectToAction(nameof(Index));
        }

        [Authorize]
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
            if (!User.IsInRole("Patient"))
                return true;

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var patient = await patientService.GetDetailsByUserIdAsync(userId);

            return patient != null && patient.Id == patientId;
        }

        private async Task<bool> CanEditPatient(int patientId)
        {            
            if (User.IsInRole("Doctor"))
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
