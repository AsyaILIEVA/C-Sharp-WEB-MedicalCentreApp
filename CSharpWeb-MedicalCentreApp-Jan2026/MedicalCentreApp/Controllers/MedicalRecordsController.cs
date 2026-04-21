using MedicalCentreApp.Services.Core.Interfaces;
using MedicalCentreApp.ViewModels.MedicalRecords;
using Microsoft.AspNetCore.Mvc;

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
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(CreateMedicalRecordViewModel model)
    {
        if (!ModelState.IsValid)
            return View(model);

        try
        {
            var medicalRecordId = await medicalRecordService.CreateAsync(model);

            if (medicalRecordId == Guid.Empty)
            {
                ModelState.AddModelError("", "Unable to create medical record.");
                return View(model);
            }

            return RedirectToAction("Create", "Prescriptions",
                new { medicalRecordId });
        }
        catch (ArgumentException ex)
        {            
            ModelState.AddModelError("", ex.Message);
            return View(model);
        }
        catch (Exception)
        {            
            return RedirectToAction("Error", "Home");
        }
    }

    [HttpGet]
    public async Task<IActionResult> Details(Guid id)
    {
        if (id == Guid.Empty)
            return BadRequest();

        try
        {
            var record = await medicalRecordService.GetDetailsAsync(id);

            if (record == null)
                return NotFound();

            return View(record);
        }
        catch (Exception)
        {
            return RedirectToAction("Error", "Home");
        }
    }
}