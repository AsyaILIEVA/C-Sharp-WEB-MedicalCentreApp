using MedicalCentreApp.Services.Core.Interfaces;
using MedicalCentreApp.ViewModels.Appointments;
using Microsoft.AspNetCore.Mvc;

namespace MedicalCentreApp.Controllers
{
    public class AppointmentsController : Controller
    {
        private readonly IAppointmentService appointmentService;

        public AppointmentsController(IAppointmentService appointmentService)
        {
            this.appointmentService = appointmentService;
        }

        public async Task<IActionResult> Index()
        {
            var appointments = await appointmentService.GetAllAsync();
            return View(appointments);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var model = await appointmentService.GetCreateModelAsync();
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> CreateForPatient(int patientId)
        {
            var model = await appointmentService.GetCreateForPatientModelAsync(patientId);
            if (model == null)
                return NotFound();

            return View("Create", model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateAppointmentViewModel model)
        {
            var result = await appointmentService.CreateAsync(model);

            if (!result.IsSuccess)
            {
                ModelState.AddModelError("", result.Error!);
                                
                var dropdowns = await appointmentService.GetCreateModelAsync();
                model.Patients = dropdowns.Patients;
                model.Doctors = dropdowns.Doctors;

                return View(model);
            }


            return RedirectToAction("Details", "Patients",
                new { id = model.PatientId });
        }

        public async Task<IActionResult> Details(int id)
        {
            var appointment = await appointmentService.GetDetailsAsync(id);
            if (appointment == null)
                return NotFound();

            return View(appointment);
        }

        [HttpGet]
        public IActionResult CreateMedicalRecord(int appointmentId)
        {
            return View(new CreateMedicalRecordViewModel
            {
                AppointmentId = appointmentId
            });
        }

        [HttpPost]
        public async Task<IActionResult> CreateMedicalRecord(CreateMedicalRecordViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            await appointmentService.CreateMedicalRecordAsync(model);

            return RedirectToAction(nameof(Details),
                new { id = model.AppointmentId });
        }
    }
}
