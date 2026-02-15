using MedicalCentreApp.Data;
using MedicalCentreApp.Data.Models;
using MedicalCentreApp.Services.Core.Interfaces;
using MedicalCentreApp.ViewModels.Appointments;
using MedicalCentreApp.ViewModels.Patients;
using Microsoft.EntityFrameworkCore;

namespace MedicalCentreApp.Services.Core
{
    public class PatientService : IPatientService
    {
        private readonly MedicalCentreAppDbContext dbContext;

        public PatientService(MedicalCentreAppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<IEnumerable<PatientListViewModel>> GetAllAsync()
        {
            return await dbContext.Patients
                .AsNoTracking()
                .Select(p => new PatientListViewModel
                {
                    Id = p.Id,
                    FirstName = p.FirstName,
                    MiddleName = p.MiddleName,
                    LastName = p.LastName
                })
                .ToListAsync();
        }

        public async Task<PatientDetailsViewModel?> GetDetailsAsync(int id)
        {
            return await dbContext.Patients
                .AsNoTracking()
                .Where(p => p.Id == id)
                .Select(p => new PatientDetailsViewModel
                {
                    Id = p.Id,
                    FirstName = p.FirstName,
                    MiddleName = p.MiddleName,
                    LastName = p.LastName,
                    EGN = p.EGN,
                    DateOfBirth = p.DateOfBirth,
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
        }

        public async Task CreateAsync(CreateEditPatientViewModel model)
        {
            Patient patient = new Patient
            {
                FirstName = model.FirstName,
                MiddleName = model.MiddleName,
                LastName = model.LastName,
                EGN = model.EGN,
                DateOfBirth = model.DateOfBirth,
                PhoneNumber = model.PhoneNumber,
                Email = model.Email,
                Address = model.Address
            };

            dbContext.Patients.Add(patient);
            await dbContext.SaveChangesAsync();
        }

        public async Task<CreateEditPatientViewModel?> GetForEditAsync(int id)
        {
            return await dbContext.Patients
                .AsNoTracking()
                .Where(p => p.Id == id)
                .Select(p => new CreateEditPatientViewModel
                {
                    Id = p.Id,
                    FirstName = p.FirstName,
                    MiddleName = p.MiddleName,
                    LastName = p.LastName,
                    EGN = p.EGN,
                    DateOfBirth = p.DateOfBirth,
                    PhoneNumber = p.PhoneNumber,
                    Email = p.Email,
                    Address = p.Address
                })
                .FirstOrDefaultAsync();
        }

        public async Task<bool> UpdateAsync(int id, CreateEditPatientViewModel model)
        {
            if (id != model.Id)
                return false;

            var patient = await dbContext.Patients.FindAsync(id);
            if (patient == null)
                return false;

            patient.FirstName = model.FirstName;
            patient.MiddleName = model.MiddleName;
            patient.LastName = model.LastName;
            patient.EGN = model.EGN;
            patient.DateOfBirth = model.DateOfBirth;
            patient.PhoneNumber = model.PhoneNumber;
            patient.Email = model.Email;
            patient.Address = model.Address;

            await dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<PatientListViewModel?> GetForDeleteAsync(int id)
        {
            return await dbContext.Patients
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
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var patient = await dbContext.Patients.FindAsync(id);
            if (patient == null)
                return false;

            dbContext.Patients.Remove(patient);
            await dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<PatientMedicalRecordViewModel>> GetMedicalRecordsAsync(int patientId)
        {
            return await dbContext.MedicalRecords
                .Where(m => m.Appointment.PatientId == patientId)
                .OrderByDescending(m => m.CreatedOn)
                .Select(m => new PatientMedicalRecordViewModel
                {
                    AppointmentDate = m.Appointment.Date,
                    DoctorName = m.Appointment.Doctor.FullName,
                    Diagnosis = m.Diagnosis,
                    Prescription = m.Prescription
                })
                .ToListAsync();
        }
    }
}
