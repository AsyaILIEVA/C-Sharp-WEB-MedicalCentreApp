using MedicalCentreApp.Data;
using MedicalCentreApp.Data.Models;
using MedicalCentreApp.Services.Core.Interfaces;
using MedicalCentreApp.ViewModels.MedicalRecords;
using MedicalCentreApp.ViewModels.Prescriptions;

using Microsoft.EntityFrameworkCore;

namespace MedicalCentreApp.Services.Core
{
    public class MedicalRecordService : IMedicalRecordService
    {
        private readonly MedicalCentreAppDbContext dbContext;

        public MedicalRecordService(MedicalCentreAppDbContext dbContext)
        {
            this.dbContext = dbContext;
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

            await dbContext.MedicalRecords.AddAsync(record);
            await dbContext.SaveChangesAsync();
        }

        public async Task<MedicalRecordDetailsViewModel?> GetDetailsAsync(Guid id)
        {
            MedicalRecordDetailsViewModel? record = await dbContext.MedicalRecords
                .AsNoTracking()
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
            bool exists = await dbContext.MedicalRecords
                .AnyAsync(r => r.Id == id);

            return exists;
        }
    }
}