using MedicalCentreApp.Data.Models;
using MedicalCentreApp.Data.Repositories.Interfaces;
using MedicalCentreApp.Services.Core.Interfaces;
using MedicalCentreApp.ViewModels.Prescriptions;
using Microsoft.EntityFrameworkCore;

namespace MedicalCentreApp.Services.Core
{
    public class PrescriptionService : IPrescriptionService
    {
        private readonly IPrescriptionRepository prescriptionRepository;

        public PrescriptionService(IPrescriptionRepository prescriptionRepository)
        {
            this.prescriptionRepository = prescriptionRepository;
        }

        public async Task<IEnumerable<PrescriptionListViewModel>> GetByMedicalRecordAsync(Guid medicalRecordId)
        {
            IEnumerable<PrescriptionListViewModel> prescriptions = await prescriptionRepository
                .AllAsNoTracking()
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

            return prescriptions;
        }

        public async Task<PrescriptionDetailsViewModel?> GetDetailsAsync(int id)
        {
            PrescriptionDetailsViewModel? prescription = await prescriptionRepository
                .AllAsNoTracking()
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

            return prescription;
        }

        public async Task CreateAsync(CreatePrescriptionViewModel model)
        {
            Prescription prescription = new Prescription
            {
                MedicationName = model.MedicationName,
                Dosage = model.Dosage,
                MedicalRecordId = model.MedicalRecordId,
                ExpirationDate = model.ExpirationDate
            };

            await prescriptionRepository.AddAsync(prescription);
            await prescriptionRepository.SaveChangesAsync();
        }
    }
}