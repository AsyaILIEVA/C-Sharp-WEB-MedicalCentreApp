using MedicalCentreApp.Data;
using MedicalCentreApp.Data.Models;

using MedicalCentreApp.ViewModels.Patients;
using MedicalCentreApp.ViewModels.Appointments;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace MedicalCentreApp.Controllers
{
    public class PatientsController : Controller
    {
        private readonly MedicalCentreAppDbContext dbContext;

        public PatientsController(MedicalCentreAppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<IActionResult> Index()
        {
            var patients = await dbContext.Patients
                .AsNoTracking()
                .Select(p => new PatientListViewModel
                {
                    Id = p.Id,
                    FirstName = p.FirstName,
                    MiddleName = p.MiddleName,
                    LastName = p.LastName
                    //EGN = p.EGN,
                    //PhoneNumber = p.PhoneNumber,
                    //Email = p.Email,
                    //Address = p.Address
                })
                .ToListAsync();

            return View(patients);
        }

        public async Task<IActionResult> Details(int id)
        {
            var patient = await dbContext.Patients
                .AsNoTracking()
                .Where(p => p.Id == id)
                .Select(p => new PatientDetailsViewModel
                {
                    Id = p.Id,
                    FirstName = p.FirstName,
                    MiddleName = p.MiddleName,
                    LastName = p.LastName,
                    EGN = p.EGN,
                    PhoneNumber = p.PhoneNumber,
                    Email = p.Email,
                    Address = p.Address,
                    Appointments = p.Appointments
                            .Where(a => a.Date >= DateTime.Now)
                            .OrderBy(a => a.Date)
                            .Select(a => new AppointmentInfoViewModel
                            {
                                   Id = a.Id,
                                   Date = a.Date,
                                   DoctorName = a.Doctor.FullName,
                                   Reason = a.Reason
                            })
                            .ToList()
                })
                .FirstOrDefaultAsync();

            if (patient == null)
            {
                return NotFound();
            }

            return View(patient);
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
            {
                return View(model);
            }

            var patient = new Patient
            {
                Id = model.Id,
                FirstName = model.FirstName,
                MiddleName = model.MiddleName,
                LastName = model.LastName,
                EGN = model.EGN,
                DateOfBirth = model.DateOfBirth,
                PhoneNumber = model.PhoneNumber,
                Email= model.Email,
                Address = model.Address
            };

            dbContext.Patients.Add(patient);
            await dbContext.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int id)
        {
            var patient = await dbContext.Patients
                .AsNoTracking()
                .Where(p => p.Id == id)
                .Select(p => new CreateEditPatientViewModel
                {
                    Id = p.Id,
                    FirstName = p.FirstName,
                    MiddleName = p.MiddleName,
                    LastName = p.LastName,
                    EGN = p.EGN,
                    PhoneNumber = p.PhoneNumber,
                    Email = p.Email,
                    Address = p.Address
                })
                .FirstOrDefaultAsync();

            if (patient == null)
            {
                return NotFound();
            }

            return View(patient);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, CreateEditPatientViewModel model)
        {
            if (id != model.Id)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var patient = await dbContext.Patients.FindAsync(id);

            if (patient == null)
            {
                return NotFound();
            }

            patient.Id = model.Id;
            patient.FirstName = model.FirstName;
            patient.MiddleName = model.MiddleName;
            patient.LastName = model.LastName;
            patient.EGN = model.EGN;
            patient.DateOfBirth = model.DateOfBirth;
            patient.PhoneNumber = model.PhoneNumber;
            patient.Email = model.Email;
            patient.Address = model.Address;

            await dbContext.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var patient = await dbContext.Patients
                .AsNoTracking()
                .Where(p => p.Id == id)
                .Select(p => new PatientListViewModel
                {
                    Id = p.Id,
                    FirstName = p.FirstName,
                    MiddleName = p.MiddleName,
                    LastName = p.LastName,
                    EGN = p.EGN,
                    PhoneNumber = p.PhoneNumber,
                    Email = p.Email,
                    Address = p.Address
                })
                .FirstOrDefaultAsync();

            if (patient == null)
            {
                return NotFound();
            }

            return View(patient);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var patient = await dbContext.Patients.FindAsync(id);

            if (patient == null)
            {
                return NotFound();
            }

            dbContext.Patients.Remove(patient);
            await dbContext.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> MedicalRecords(int id)
        {
            var records = await dbContext.MedicalRecords
                .Where(m => m.Appointment.PatientId == id)
                .OrderByDescending(m => m.CreatedOn)
                .Select(m => new PatientMedicalRecordViewModel
                {
                    AppointmentDate = m.Appointment.Date,
                    DoctorName = m.Appointment.Doctor.FullName,
                    Diagnosis = m.Diagnosis,
                    Prescription = m.Prescription
                })
                .ToListAsync();

            ViewBag.PatientId = id;

            return View(records);
        }

    }
}
