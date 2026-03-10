using MedicalCentreApp.Data.Models;

namespace MedicalCentreApp.Data.Repositories.Interfaces
{
    public interface IAppointmentRepository
    {
        IQueryable<Appointment> All();

        IQueryable<Appointment> AllAsNoTracking();

        Task<Appointment?> GetByIdAsync(int id);

        Task AddAsync(Appointment appointment);

        void Update(Appointment appointment);

        void Delete(Appointment appointment);

        Task<int> SaveChangesAsync();
    }
}