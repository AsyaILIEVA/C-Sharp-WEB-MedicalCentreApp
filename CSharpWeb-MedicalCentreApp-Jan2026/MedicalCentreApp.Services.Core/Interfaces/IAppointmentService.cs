using MedicalCentreApp.ViewModels.Appointments;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace MedicalCentreApp.Services.Core.Interfaces
{
    public interface IAppointmentService
    {
        Task<IEnumerable<AppointmentListViewModel>> GetAllAsync();

        Task<CreateAppointmentViewModel> GetCreateModelAsync();

        Task<CreateAppointmentViewModel?> GetCreateForPatientModelAsync(int patientId);

        Task<(bool IsSuccess, string? Error)> CreateAsync(CreateAppointmentViewModel model);

        Task<AppointmentDetailsViewModel?> GetDetailsAsync(int id);

        Task CreateMedicalRecordAsync(CreateMedicalRecordViewModel model);
    }
}
