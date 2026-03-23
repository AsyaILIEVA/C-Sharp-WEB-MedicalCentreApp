using MedicalCentreApp.Data.Models;
using MedicalCentreApp.Data.Repositories.Interfaces;
using MedicalCentreApp.Services.Core.Interfaces;
using MedicalCentreApp.ViewModels.Appointments;
using MedicalCentreApp.ViewModels.Patients;
using MedicalCentreApp.ViewModels.Prescriptions;
using Microsoft.EntityFrameworkCore;

namespace MedicalCentreApp.Services.Core
{
    public class PatientService : IPatientService
    {
        private readonly IPatientRepository patientRepository;

        public PatientService(IPatientRepository patientRepository)
        {
            this.patientRepository = patientRepository;
        }

        public async Task<IEnumerable<PatientListViewModel>> GetAllAsync()
        {
            IEnumerable<PatientListViewModel> patients = await patientRepository
                .AllAsNoTracking()
                .Select(p => new PatientListViewModel
                {
                    Id = p.Id,
                    FirstName = p.FirstName,
                    MiddleName = p.MiddleName,
                    LastName = p.LastName
                })
                .ToListAsync();

            return patients;
        }

        public async Task<PatientDetailsViewModel?> GetDetailsAsync(int id)
        {
            PatientDetailsViewModel? detailsViewModel = await patientRepository
                .AllAsNoTracking()
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

            return detailsViewModel;
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

            await patientRepository.AddAsync(patient);
            await patientRepository.SaveChangesAsync();
        }

        public async Task<CreateEditPatientViewModel?> GetForEditAsync(int id)
        {
            CreateEditPatientViewModel? editViewModel = await patientRepository
                .AllAsNoTracking()
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

            return editViewModel;
        }

        public async Task<bool> UpdateAsync(int id, CreateEditPatientViewModel model)
        {
            if (id != model.Id) return false;

            var patient = await patientRepository.GetByIdAsync(id);
            if (patient == null) return false;

            patient.FirstName = model.FirstName;
            patient.MiddleName = model.MiddleName;
            patient.LastName = model.LastName;
            patient.EGN = model.EGN;
            patient.DateOfBirth = model.DateOfBirth;
            patient.PhoneNumber = model.PhoneNumber;
            patient.Email = model.Email;
            patient.Address = model.Address;

            patientRepository.Update(patient);
            await patientRepository.SaveChangesAsync();

            return true;
        }

        public async Task<PatientListViewModel?> GetForDeleteAsync(int id)
        {
            PatientListViewModel? deleteViewModel = await patientRepository
                .AllAsNoTracking()
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

            return deleteViewModel;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var patient = await patientRepository.GetByIdAsync(id);
            if (patient == null) return false;

            patientRepository.Delete(patient);
            await patientRepository.SaveChangesAsync();

            return true;
        }

        public async Task<IEnumerable<PatientMedicalRecordViewModel>> GetMedicalRecordsAsync(int patientId)
        {
            IEnumerable<PatientMedicalRecordViewModel> records = await patientRepository
                .AllAsNoTracking()
                .Where(p => p.Id == patientId)
                .SelectMany(p => p.Appointments)
                .Where(a => a.MedicalRecord != null)
                .Select(a => new PatientMedicalRecordViewModel
                {
                    AppointmentDate = a.Date,
                    DoctorName = a.Doctor.FullName,
                    Diagnosis = a.MedicalRecord.Diagnosis,
                    Prescriptions = a.MedicalRecord.Prescriptions
                        .Select(p => new PrescriptionListViewModel
                        {
                            MedicationName = p.MedicationName,
                            Dosage = p.Dosage
                        })
                        .ToList()
                })
                .OrderByDescending(r => r.AppointmentDate)
                .ToListAsync();

            return records;
        }

        public async Task<PatientDetailsViewModel?> GetDetailsByUserIdAsync(string userId)
        {
            return await patientRepository
                 .AllAsNoTracking()
                 .Where(p => p.UserId == userId)
                 .Select(p => new PatientDetailsViewModel {
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

        public async Task CreateFromUserAsync(string userId, string email)
        {
            var patient = new Patient
            {
                UserId = userId,
                Email = email
            };

            await patientRepository.AddAsync(patient);
            await patientRepository.SaveChangesAsync();
        }

        public async Task<PatientProfileViewModel?> GetProfileByUserIdAsync(string userId)
        {
            return await patientRepository.AllAsNoTracking()
                .Where(p => p.UserId == userId)
                .Select(p => new PatientProfileViewModel
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

        public async Task<bool> UpdateProfileAsync(string userId, PatientProfileViewModel model)
        {
            var patient = await patientRepository
                .All()
                .FirstOrDefaultAsync(p => p.UserId == userId);
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

            patientRepository.Update(patient);
            await patientRepository.SaveChangesAsync();

            return true;
        }
    }
}