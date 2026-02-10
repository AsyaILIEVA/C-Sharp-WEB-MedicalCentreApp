using MedicalCentreApp.Data;
using MedicalCentreApp.Models;
using MedicalCentreApp.Models.Enums;
using MedicalCentreApp.ViewModels.Appointments;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace MedicalCentreApp.Controllers
{
    //[Authorize(Roles = "Admin,Doctor")]
    public class AppointmentsController : Controller
    {
        private readonly MedicalCentreAppDbContext dbContext;

        public AppointmentsController(MedicalCentreAppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<IActionResult> Index()
        {
            var appointments = await dbContext.Appointments
                .AsNoTracking()
                .Select(a => new AppointmentListViewModel
                {
                    Id = a.Id,
                    Date = a.Date,
                    PatientName = a.Patient.FirstName + " " + a.Patient.LastName,
                    DoctorName = a.Doctor.FullName,
                    Status = a.AppointmentStatus.ToString()
                })
                .OrderBy(a => a.Date)
                .ToListAsync();

            return View(appointments);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var model = new CreateAppointmentViewModel
            {
                Date = DateTime.Now.AddDays(1),

                Patients = await dbContext.Patients
                    .AsNoTracking()
                    .Select(p => new SelectListItem
                    {
                        Value = p.Id.ToString(),
                        Text = $"{p.FirstName} {p.LastName}"
                    })
                    .ToListAsync(),

                Doctors = await dbContext.Doctors
                    .AsNoTracking()
                    .Select(d => new SelectListItem
                    {
                        Value = d.Id.ToString(),
                        Text = d.FullName
                    })
                    .ToListAsync()
            };

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> CreateForPatient(int patientId)
        {
            var patient = await dbContext.Patients
                .AsNoTracking()
                .Where(p => p.Id == patientId)
                .Select(p => new
                {
                    p.Id,
                    FullName = p.FirstName + " " + p.LastName
                })
                .FirstOrDefaultAsync();

            if (patient == null)
            {
                return NotFound();
            }

            var model = new CreateAppointmentViewModel
            {
                PatientId = patient.Id,
                PatientName = patient.FullName,
                Date = DateTime.Today.AddDays(1),

                Doctors = await dbContext.Doctors
                    .Select(d => new SelectListItem
                    {
                        Value = d.Id.ToString(),
                        Text = d.FullName
                    })
                    .ToListAsync()
            };

            return View("Create", model);
        }


        [HttpPost]
        public async Task<IActionResult> Create(CreateAppointmentViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.Patients = await dbContext.Patients
                    .AsNoTracking()
                    .Select(p => new SelectListItem
                    {
                        Value = p.Id.ToString(),
                        Text = $"{p.FirstName} {p.LastName}"
                    })
                    .ToListAsync();

                model.Doctors = await dbContext.Doctors
                    .AsNoTracking()
                    .Select(d => new SelectListItem
                    {
                        Value = d.Id.ToString(),
                        Text = d.FullName
                    })
                    .ToListAsync();

                return View(model);
            }

            if (model.Time < TimeSpan.FromHours(8) ||
                model.Time > TimeSpan.FromHours(18))
            {
                ModelState.AddModelError(nameof(model.Time),
                    "Appointments are available between 08:00 and 18:00.");
            }

            var appointmentDateTime = model.Date.Date + model.Time;

            var appointment = new Appointment
            {
                Date = appointmentDateTime,
                Reason = model.Reason,
                PatientId = model.PatientId,
                DoctorId = model.DoctorId,
                AppointmentStatus = AppointmentStatus.Scheduled
            };

            dbContext.Appointments.Add(appointment);
            await dbContext.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Details(int id)
        {
            var appointment = await dbContext.Appointments
                .AsNoTracking()
                .Where(a => a.Id == id)
                .Select(a => new AppointmentDetailsViewModel
                {
                    Date = a.Date,
                    Reason = a.Reason,
                    Status = a.AppointmentStatus.ToString(),
                    PatientName = a.Patient.FirstName + " " + a.Patient.LastName,
                    DoctorName = a.Doctor.FullName,
                    HasMedicalRecord = a.MedicalRecord != null
                })
                .FirstOrDefaultAsync();

            if (appointment == null)
            {
                return NotFound();
            }

            return View(appointment);
        }
    }
}
