using MedicalCentreApp.Data;
using MedicalCentreApp.Data.Models;
using MedicalCentreApp.Data.Models.Enums;
using MedicalCentreApp.Services.Core.Interfaces;
using MedicalCentreApp.ViewModels.Appointments;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace MedicalCentreApp.Services.Core
{
    public class AppointmentService : IAppointmentService
    {
        private readonly MedicalCentreAppDbContext dbContext;

        public AppointmentService(MedicalCentreAppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<IEnumerable<AppointmentListViewModel>> GetAllAsync()
        {
            return await dbContext.Appointments
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
        }

        public async Task<CreateAppointmentViewModel> GetCreateModelAsync()
        {
            return new CreateAppointmentViewModel
            {
                Date = DateTime.Now.AddDays(1),

                Patients = await dbContext.Patients
                    .AsNoTracking()
                    .Select(p => new SelectListItem
                    {
                        Value = p.Id.ToString(),
                        Text = p.FirstName + " " + p.LastName
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
        }

        public async Task<CreateAppointmentViewModel?> GetCreateForPatientModelAsync(int patientId)
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
                return null;

            return new CreateAppointmentViewModel
            {
                PatientId = patient.Id,
                Date = DateTime.Today.AddDays(1),

                Patients = new List<SelectListItem>
                {
                    new SelectListItem
                    {
                        Value = patient.Id.ToString(),
                        Text = patient.FullName
                    }
                },

                Doctors = await dbContext.Doctors
                    .AsNoTracking()
                    .Select(d => new SelectListItem
                    {
                        Value = d.Id.ToString(),
                        Text = d.FullName
                    })
                    .ToListAsync()
            };
        }

        public async Task<(bool IsSuccess, string? Error)> CreateAsync(CreateAppointmentViewModel model)
        {
            if (model.Time < TimeSpan.FromHours(8) ||
                model.Time > TimeSpan.FromHours(18))
            {
                return (false, "Appointments are available between 08:00 and 18:00.");
            }

            var appointmentDateTime = model.Date.Date + model.Time;

            var isDoctorBusy = await dbContext.Appointments
                .AnyAsync(a => a.DoctorId == model.DoctorId &&
                               a.Date == appointmentDateTime);

            if (isDoctorBusy)
            {
                return (false, "This doctor already has an appointment at this time.");
            }

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

            return (true, null);
        }

        public async Task<AppointmentDetailsViewModel?> GetDetailsAsync(int id)
        {
            return await dbContext.Appointments
                .AsNoTracking()
                .Where(a => a.Id == id)
                .Select(a => new AppointmentDetailsViewModel
                {
                    Id = a.Id,
                    Date = a.Date,
                    Reason = a.Reason,
                    Status = a.AppointmentStatus.ToString(),
                    PatientName = a.Patient.FirstName + " " + a.Patient.LastName,
                    DoctorName = a.Doctor.FullName,
                    HasMedicalRecord = a.MedicalRecord != null,
                    MedicalRecordId = a.MedicalRecord != null ? a.MedicalRecord.Id : null,
                    Diagnosis = a.MedicalRecord != null ? a.MedicalRecord.Diagnosis : null,
                    Prescription = a.MedicalRecord != null ? a.MedicalRecord.Prescription : null
                })
                .FirstOrDefaultAsync();
        }

        public async Task CreateMedicalRecordAsync(CreateMedicalRecordViewModel model)
        {
            var record = new MedicalRecord
            {
                Id = Guid.NewGuid(),
                AppointmentId = model.AppointmentId,
                Diagnosis = model.Diagnosis,
                Prescription = model.Prescription,
                CreatedOn = DateTime.UtcNow
            };

            dbContext.MedicalRecords.Add(record);

            var appointment = await dbContext.Appointments
                .FindAsync(model.AppointmentId);

            if (appointment != null)
            {
                appointment.AppointmentStatus = AppointmentStatus.Completed;
            }

            await dbContext.SaveChangesAsync();
        }
    }
}
