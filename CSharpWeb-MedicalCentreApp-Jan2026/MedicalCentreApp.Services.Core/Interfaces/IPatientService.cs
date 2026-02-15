using MedicalCentreApp.ViewModels.Patients;

namespace MedicalCentreApp.Services.Core.Interfaces
{
    public interface IPatientService
    {
        Task<IEnumerable<PatientListViewModel>> GetAllAsync();

        Task<PatientDetailsViewModel?> GetDetailsAsync(int id);

        Task CreateAsync(CreateEditPatientViewModel model);

        Task<CreateEditPatientViewModel?> GetForEditAsync(int id);

        Task<bool> UpdateAsync(int id, CreateEditPatientViewModel model);

        Task<PatientListViewModel?> GetForDeleteAsync(int id);

        Task<bool> DeleteAsync(int id);

        Task<IEnumerable<PatientMedicalRecordViewModel>> GetMedicalRecordsAsync(int patientId);
    }
}
