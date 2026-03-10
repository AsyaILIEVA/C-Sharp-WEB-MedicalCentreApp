using MedicalCentreApp.Data.Models;
using MedicalCentreApp.Data.Models.Enums;
using MedicalCentreApp.Data.Repositories.Interfaces;
using MedicalCentreApp.Services.Core.Interfaces;
using MedicalCentreApp.ViewModels.Appointments;
using MedicalCentreApp.ViewModels.MedicalRecords;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace MedicalCentreApp.Services.Core
{
    public class AppointmentService : IAppointmentService
    {
        private readonly IAppointmentRepository appointmentRepository;
        private readonly IPatientRepository patientRepository;
        private readonly IDoctorRepository doctorRepository;
        private readonly IMedicalRecordRepository medicalRecordRepository;

        public AppointmentService(
            IAppointmentRepository appointmentRepository,
            IPatientRepository patientRepository,
            IDoctorRepository doctorRepository,
            IMedicalRecordRepository medicalRecordRepository)
        {
            this.appointmentRepository = appointmentRepository;
            this.patientRepository = patientRepository;
            this.doctorRepository = doctorRepository;
            this.medicalRecordRepository = medicalRecordRepository;
        }

        public async Task<IEnumerable<AppointmentListViewModel>> GetAllAsync()
        {
            IEnumerable<AppointmentListViewModel> appointments = await appointmentRepository
                .AllAsNoTracking()
                .Select(a => new AppointmentListViewModel
                {
                    Id = a.Id,
                    Date = a.Date,
                    PatientName = a.Patient.FirstName + " " + a.Patient.LastName,
                    DoctorName = a.Doctor.FullName,
                    Status = a.AppointmentStatus
                })
                .OrderBy(a => a.Date)
                .ToListAsync();

            return appointments;
        }

        public async Task<CreateAppointmentViewModel> GetCreateModelAsync()
        {
            CreateAppointmentViewModel model = new CreateAppointmentViewModel
            {
                Date = DateTime.Now.AddDays(1),

                Patients = await patientRepository
                    .AllAsNoTracking()
                    .Select(p => new SelectListItem
                    {
                        Value = p.Id.ToString(),
                        Text = p.FirstName + " " + p.LastName
                    })
                    .ToListAsync(),

                Doctors = await doctorRepository
                    .AllAsNoTracking()
                    .Select(d => new SelectListItem
                    {
                        Value = d.Id.ToString(),
                        Text = d.FullName
                    })
                    .ToListAsync()
            };

            return model;
        }

        public async Task<CreateAppointmentViewModel?> GetCreateForPatientModelAsync(int patientId)
        {
            var patient = await patientRepository
                .AllAsNoTracking()
                .Where(p => p.Id == patientId)
                .Select(p => new { p.Id, FullName = p.FirstName + " " + p.LastName })
                .FirstOrDefaultAsync();

            if (patient == null)
                return null;

            CreateAppointmentViewModel model = new CreateAppointmentViewModel
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

                Doctors = await doctorRepository
                    .AllAsNoTracking()
                    .Select(d => new SelectListItem
                    {
                        Value = d.Id.ToString(),
                        Text = d.FullName
                    })
                    .ToListAsync()
            };

            return model;
        }

        public async Task<(bool IsSuccess, string? Error)> CreateAsync(CreateAppointmentViewModel model)
        {
            if (model.Time < TimeSpan.FromHours(8) || model.Time > TimeSpan.FromHours(18))
                return (false, "Appointments are available between 08:00 and 18:00.");

            var appointmentDateTime = model.Date.Date + model.Time;

            bool isDoctorBusy = await appointmentRepository
                .All()
                .AnyAsync(a => a.DoctorId == model.DoctorId && a.Date == appointmentDateTime);

            if (isDoctorBusy)
                return (false, "This doctor already has an appointment at this time.");

            Appointment appointment = new Appointment
            {
                Date = appointmentDateTime,
                Reason = model.Reason,
                PatientId = model.PatientId,
                DoctorId = model.DoctorId,
                AppointmentStatus = AppointmentStatus.Scheduled
            };

            await appointmentRepository.AddAsync(appointment);
            await appointmentRepository.SaveChangesAsync();

            return (true, null);
        }

        public async Task<AppointmentDetailsViewModel?> GetDetailsAsync(int id)
        {
            AppointmentDetailsViewModel? details = await appointmentRepository
                .AllAsNoTracking()
                .Where(a => a.Id == id)
                .Select(a => new AppointmentDetailsViewModel
                {
                    Id = a.Id,
                    Date = a.Date,
                    Reason = a.Reason,
                    Status = a.AppointmentStatus,
                    PatientName = a.Patient.FirstName + " " + a.Patient.LastName,
                    DoctorName = a.Doctor.FullName,
                    HasMedicalRecord = a.MedicalRecord != null,
                    MedicalRecordId = a.MedicalRecord != null ? a.MedicalRecord.Id : null,
                    Diagnosis = a.MedicalRecord != null ? a.MedicalRecord.Diagnosis : null,

                    Prescriptions = a.MedicalRecord != null
                        ? a.MedicalRecord.Prescriptions
                            .Select(p => p.MedicationName)
                            .ToList()
                        : new List<string>()
                })
                .FirstOrDefaultAsync();

            return details;
        }

        public async Task CreateMedicalRecordAsync(CreateMedicalRecordViewModel model)
        {
            var record = new MedicalRecord
            {
                Id = Guid.NewGuid(),
                AppointmentId = model.AppointmentId,
                Diagnosis = model.Diagnosis,
                CreatedOn = DateTime.UtcNow
            };

            await medicalRecordRepository.AddAsync(record);

            var appointment = await appointmentRepository.GetByIdAsync(model.AppointmentId);
            if (appointment != null)
            {
                appointment.AppointmentStatus = AppointmentStatus.Completed;
                appointmentRepository.Update(appointment);
            }

            await medicalRecordRepository.SaveChangesAsync();
            await appointmentRepository.SaveChangesAsync();
        }
    }
}