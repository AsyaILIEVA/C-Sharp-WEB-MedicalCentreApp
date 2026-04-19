using MedicalCentreApp.Services.Core.Interfaces;
using MedicalCentreApp.ViewModels.Patients;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MedicalCentreApp.Areas.Administrator.Controllers
{
    [Area("Administrator")]
    [Authorize(Roles = "Administrator")]
    public class PatientsManagementController : Controller
    {
        private readonly IPatientService patientService;

        public PatientsManagementController(IPatientService patientService)
        {
            this.patientService = patientService;
        }

        
        public async Task<IActionResult> Index(int page = 1, int pageSize = 10)
        {
            var result = await patientService.GetPagedAsync(page, pageSize);
            return View(result);
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

        
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var model = await patientService.GetForEditAsync(id);

            if (model == null)
                return NotFound();

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, CreateEditPatientViewModel model)
        {
            if (id != model.Id)
                return BadRequest();

            if (!ModelState.IsValid)
                return View(model);

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
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var success = await patientService.DeleteAsync(id);

            if (!success)
                return NotFound();

            return RedirectToAction(nameof(Index));
        }
    }
}