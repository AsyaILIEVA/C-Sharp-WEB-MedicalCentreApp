using MedicalCentreApp.Services.Core.Interfaces;
using MedicalCentreApp.ViewModels.Prescriptions;
using Microsoft.AspNetCore.Mvc;

namespace MedicalCentreApp.Controllers
{
    public class PrescriptionsController : Controller
    {
        private readonly IPrescriptionService prescriptionService;

        public PrescriptionsController(IPrescriptionService prescriptionService)
        {
            this.prescriptionService = prescriptionService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var prescriptions = await prescriptionService.GetAllAsync();
            return View(prescriptions);
        }

        [HttpGet]
        public IActionResult Create(Guid medicalRecordId)
        {
            if (medicalRecordId == Guid.Empty)
                return BadRequest();

            var model = new CreatePrescriptionViewModel
            {
                MedicalRecordId = medicalRecordId
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreatePrescriptionViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            try
            {
                await prescriptionService.CreateAsync(model);
            }
            catch (ArgumentException ex)
            {                
                ModelState.AddModelError(string.Empty, ex.Message);
                return View(model);
            }

            return RedirectToAction("Details", "MedicalRecords",
                new { id = model.MedicalRecordId });
        }
    }
}