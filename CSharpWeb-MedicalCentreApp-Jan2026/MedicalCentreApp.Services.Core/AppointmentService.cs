using MedicalCentreApp.Data.Models;
using MedicalCentreApp.Data.Models.Enums;
using MedicalCentreApp.Data.Repositories.Interfaces;
using MedicalCentreApp.Services.Core.Interfaces;
using MedicalCentreApp.ViewModels.Appointments;
using MedicalCentreApp.ViewModels.MedicalRecords;
using MedicalCentreApp.ViewModels.Prescriptions;
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
            var appointments = await appointmentRepository
                .All()
                .Include(a => a.Patient)
                .Include(a => a.Doctor)
                .ToListAsync();

            bool hasChanges = false;

            foreach (var appointment in appointments)
            {
                if (appointment.Date < DateTime.Now &&
                    appointment.AppointmentStatus == AppointmentStatus.Scheduled)
                {
                    appointment.AppointmentStatus = AppointmentStatus.Completed;
                    hasChanges = true;
                }
            }

            if (hasChanges)
            {
                await appointmentRepository.SaveChangesAsync();
            }

            return appointments
                .OrderBy(a => a.Date)
                .Select(a => new AppointmentListViewModel
                {
                    Id = a.Id,
                    Date = a.Date,
                    PatientName = a.Patient.FirstName + " " + a.Patient.LastName,
                    DoctorName = a.Doctor.FullName,
                    Status = a.AppointmentStatus
                })
                .ToList();
        }

        public async Task<IEnumerable<AppointmentListViewModel>> GetByPatientAsync(string userId)
        {
            var appointments = await appointmentRepository
                .All()
                .Include(a => a.Patient)
                .Include(a => a.Doctor)
                .Where(a => a.Patient.UserId == userId) 
                .ToListAsync();

            bool hasChanges = false;

            foreach (var appointment in appointments)
            {
                if (appointment.Date < DateTime.Now &&
                    appointment.AppointmentStatus == AppointmentStatus.Scheduled)
                {
                    appointment.AppointmentStatus = AppointmentStatus.Completed;
                    hasChanges = true;
                }
            }

            if (hasChanges)
            {
                await appointmentRepository.SaveChangesAsync();
            }

            return appointments
                .OrderBy(a => a.Date)
                .Select(a => new AppointmentListViewModel
                {
                    Id = a.Id,
                    Date = a.Date,
                    PatientName = a.Patient.FirstName + " " + a.Patient.LastName,
                    DoctorName = a.Doctor.FullName,
                    Status = a.AppointmentStatus
                })
                .ToList();
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
            var appointment = await appointmentRepository
                .AllAsNoTracking()
                .Include(a => a.Patient)
                .Include(a => a.Doctor)
                .Include(a => a.MedicalRecord)
                    .ThenInclude(m => m.Prescriptions)
                .FirstOrDefaultAsync(a => a.Id == id);

            if (appointment == null)
            {
                return null;
            }

            return new AppointmentDetailsViewModel
            {
                Id = appointment.Id,
                Date = appointment.Date,

                PatientName = appointment.Patient != null
                    ? appointment.Patient.FirstName + " " + appointment.Patient.LastName
                    : "No patient",

                DoctorName = appointment.Doctor != null
                    ? appointment.Doctor.FullName
                    : "No doctor",

                Status = appointment.AppointmentStatus,
                Reason = appointment.Reason,

                HasMedicalRecord = appointment.MedicalRecord != null,
                Diagnosis = appointment.MedicalRecord?.Diagnosis,

                Prescriptions = appointment.MedicalRecord?.Prescriptions != null
                    ? appointment.MedicalRecord.Prescriptions
                        .Select(p => new PrescriptionListViewModel
                        {
                            Id = p.Id,
                            MedicationName = p.MedicationName,
                            Dosage = p.Dosage,
                            IssuedOn = p.IssuedOn
                        })
                        .ToList()
                    : new List<PrescriptionListViewModel>()
            };
        
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