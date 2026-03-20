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

        [HttpGet]
        public IActionResult Create()
        {
            return View(new CreateEditPatientViewModel
            {
                DateOfBirth = DateTime.Today
            });
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateEditPatientViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            await patientService.CreateAsync(model);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int id)
        {
            var model = await patientService.GetForEditAsync(id);
            if (model == null)
                return NotFound();

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id,CreateEditPatientViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var success = await patientService.UpdateAsync(id, model);
            if (!success)
                return BadRequest();

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var patient = await patientService.GetForDeleteAsync(id);
            if (patient == null)
                return NotFound();

            return View(patient);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var success = await patientService.DeleteAsync(id);
            if (!success)
                return NotFound();

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> MedicalRecords(int id)
        {
            var records = await patientService.GetMedicalRecordsAsync(id);
            ViewBag.PatientId = id;
            return View(records);
        }
    }
}
