using MedicalCentreApp.Services.Core.Interfaces;
using MedicalCentreApp.ViewModels.MedicalRecords;
using Microsoft.AspNetCore.Mvc;

namespace MedicalCentreApp.Controllers
{
    public class MedicalRecordsController : Controller
    {
        private readonly IMedicalRecordService medicalRecordService;

        public MedicalRecordsController(IMedicalRecordService medicalRecordService)
        {
            this.medicalRecordService = medicalRecordService;
        }

        [HttpGet]
        public IActionResult Create(int appointmentId)
        {
            return View(new CreateMedicalRecordViewModel
            {
                AppointmentId = appointmentId
            });
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateMedicalRecordViewModel model)
        {
            //if (!ModelState.IsValid)
            //    return View(model);

            //await medicalRecordService.CreateAsync(model);

            //return RedirectToAction("Details", "Appointments",
            //    new { id = model.AppointmentId });
            
                if (!ModelState.IsValid)
                    return View(model);

                var medicalRecordId = await medicalRecordService.CreateAsync(model);

                // ✅ Redirect to add prescription AFTER record exists
                return RedirectToAction("Create", "Prescriptions",
                    new { medicalRecordId = medicalRecordId });
            
        }

        public async Task<IActionResult> Details(Guid id)
        {
            var record = await medicalRecordService.GetDetailsAsync(id);

            if (record == null)
                return NotFound();

            return View(record);
        }
    }


}
