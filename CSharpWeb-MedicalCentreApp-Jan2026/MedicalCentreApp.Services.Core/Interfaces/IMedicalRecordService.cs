using MedicalCentreApp.ViewModels.MedicalRecords;

namespace MedicalCentreApp.Services.Core.Interfaces
{
    public interface IMedicalRecordService
    {
        Task CreateAsync(CreateMedicalRecordViewModel model);

        Task<MedicalRecordDetailsViewModel?> GetDetailsAsync(Guid id);

        Task<bool> ExistsAsync(Guid id);
    }
}