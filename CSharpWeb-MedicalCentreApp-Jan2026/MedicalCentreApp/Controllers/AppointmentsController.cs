using MedicalCentreApp.Services.Core.Interfaces;
using MedicalCentreApp.ViewModels.Appointments;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace MedicalCentreApp.Controllers
{
    [Authorize]
    public class AppointmentsController : Controller
    {
        private readonly IAppointmentService appointmentService;

        public AppointmentsController(IAppointmentService appointmentService)
        {
            this.appointmentService = appointmentService;
        }

        public async Task<IActionResult> Index()
        {
            if (User.IsInRole("Administrator") || User.IsInRole("Doctor"))
            {
                var all = await appointmentService.GetAllAsync();
                return View(all);
            }

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null)
                return Forbid();

            var mine = await appointmentService.GetByPatientAsync(userId);

            return View(mine);
        }

        [HttpGet]
        [Authorize(Roles = "Patient,Administrator,Doctor")]
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
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateAppointmentViewModel model)
        {
            if (!ModelState.IsValid)
            {
                var dropdowns = await appointmentService.GetCreateModelAsync();
                model.Patients = dropdowns.Patients;
                model.Doctors = dropdowns.Doctors;

                return View(model);
            }

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
        
    }
}
