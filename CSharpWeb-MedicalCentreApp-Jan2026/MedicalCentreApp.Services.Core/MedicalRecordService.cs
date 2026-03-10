using MedicalCentreApp.Data.Models;
using MedicalCentreApp.Data.Repositories.Interfaces;
using MedicalCentreApp.Services.Core.Interfaces;
using MedicalCentreApp.ViewModels.MedicalRecords;
using MedicalCentreApp.ViewModels.Prescriptions;
using Microsoft.EntityFrameworkCore;

namespace MedicalCentreApp.Services.Core
{
    public class MedicalRecordService : IMedicalRecordService
    {
        private readonly IMedicalRecordRepository medicalRecordRepository;

        public MedicalRecordService(IMedicalRecordRepository medicalRecordRepository)
        {
            this.medicalRecordRepository = medicalRecordRepository;
        }

        public async Task CreateAsync(CreateMedicalRecordViewModel model)
        {
            MedicalRecord record = new MedicalRecord
            {
                Id = Guid.NewGuid(),
                AppointmentId = model.AppointmentId,
                Diagnosis = model.Diagnosis,
                CreatedOn = DateTime.UtcNow
            };

            await medicalRecordRepository.AddAsync(record);
            await medicalRecordRepository.SaveChangesAsync();
        }

        public async Task<MedicalRecordDetailsViewModel?> GetDetailsAsync(Guid id)
        {
            MedicalRecordDetailsViewModel? record = await medicalRecordRepository
                .AllAsNoTracking()
                .Where(r => r.Id == id)
                .Select(r => new MedicalRecordDetailsViewModel
                {
                    Id = r.Id,
                    AppointmentId = r.AppointmentId,
                    Diagnosis = r.Diagnosis,
                    CreatedOn = r.CreatedOn,
                    Prescriptions = r.Prescriptions
                        .Select(p => new PrescriptionListViewModel
                        {
                            Id = p.Id,
                            MedicationName = p.MedicationName,
                            Dosage = p.Dosage,
                            IssuedOn = p.IssuedOn
                        })
                        .ToList()
                })
                .FirstOrDefaultAsync();

            return record;
        }

        public async Task<bool> ExistsAsync(Guid id)
        {
            bool exists = await medicalRecordRepository
                .All()
                .AnyAsync(r => r.Id == id);

            return exists;
        }
    }
}