using MedicalCentreApp.Data;
using MedicalCentreApp.Data.Models;
using MedicalCentreApp.Services.Core.Interfaces;
using MedicalCentreApp.ViewModels.Prescriptions;
using Microsoft.EntityFrameworkCore;

namespace MedicalCentreApp.Services.Core
{
    public class PrescriptionService : IPrescriptionService
    {
        private readonly MedicalCentreAppDbContext dbContext;

        public PrescriptionService(MedicalCentreAppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<IEnumerable<PrescriptionListViewModel>> GetByMedicalRecordAsync(Guid medicalRecordId)
        {
            return await dbContext.Prescriptions
                .AsNoTracking()
                .Where(p => p.MedicalRecordId == medicalRecordId)
                .OrderByDescending(p => p.IssuedOn)
                .Select(p => new PrescriptionListViewModel
                {
                    Id = p.Id,
                    MedicationName = p.MedicationName,
                    Dosage = p.Dosage,
                    IssuedOn = p.IssuedOn
                })
                .ToListAsync();
        }

        public async Task<PrescriptionDetailsViewModel?> GetDetailsAsync(int id)
{
    return await dbContext.Prescriptions
        .AsNoTracking()
        .Where(p => p.Id == id)
        .Select(p => new PrescriptionDetailsViewModel
        {
            Id = p.Id,
            MedicationName = p.MedicationName,
            Dosage = p.Dosage,
            IssuedOn = p.IssuedOn,
            ExpirationDate = p.ExpirationDate
        })
        .FirstOrDefaultAsync();
}

public async Task CreateAsync(CreatePrescriptionViewModel model)
{
    var prescription = new Prescription
    {
        MedicationName = model.MedicationName,
        Dosage = model.Dosage,
        MedicalRecordId = model.MedicalRecordId,
        ExpirationDate = model.ExpirationDate
    };

    await dbContext.Prescriptions.AddAsync(prescription);
    await dbContext.SaveChangesAsync();
}
    }
}
