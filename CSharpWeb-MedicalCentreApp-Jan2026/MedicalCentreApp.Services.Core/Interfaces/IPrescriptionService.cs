using MedicalCentreApp.ViewModels.Prescriptions;

namespace MedicalCentreApp.Services.Core.Interfaces
{
    public interface IPrescriptionService
    {
        Task<IEnumerable<PrescriptionListViewModel>> GetByMedicalRecordAsync(Guid medicalRecordId);

        Task<PrescriptionDetailsViewModel?> GetDetailsAsync(int id);

        Task CreateAsync(CreatePrescriptionViewModel model);
    }
}
