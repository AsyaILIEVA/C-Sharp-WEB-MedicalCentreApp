using MedicalCentreApp.ViewModels.Patients;

namespace MedicalCentreApp.Services.Core.Interfaces
{
    public interface IPatientService
    {
        Task<IEnumerable<PatientListViewModel>> GetAllAsync();

        Task<PatientDetailsViewModel?> GetDetailsByUserIdAsync(string userId);

        Task<PatientDetailsViewModel?> GetDetailsAsync(int id);

        Task CreateAsync(CreateEditPatientViewModel model);

        Task CreateFromUserAsync(string userId, string email);

        Task<CreateEditPatientViewModel?> GetForEditAsync(int id);

        Task<bool> UpdateAsync(int id, CreateEditPatientViewModel model);

        Task<PatientListViewModel?> GetForDeleteAsync(int id);

        Task<bool> DeleteAsync(int id);

        Task<IEnumerable<PatientMedicalRecordViewModel>> GetMedicalRecordsAsync(int patientId);
    }
}
