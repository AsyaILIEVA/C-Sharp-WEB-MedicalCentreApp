using MedicalCentreApp.Data.Models;

namespace MedicalCentreApp.Data.Repositories.Interfaces
{
    public interface IDoctorRepository
    {
        IQueryable<Doctor> All();
        IQueryable<Doctor> AllAsNoTracking();

        Task<Doctor?> GetByIdAsync(int id);

        Task AddAsync(Doctor doctor);

        void Update(Doctor doctor);

        void Delete(Doctor doctor);

        Task<int> SaveChangesAsync();

        Task<Doctor?> GetDoctorWithAppointmentsAsync(int id);

        Task<PagedList<Doctor>> GetDoctorsAsync(int pageNumber, int pageSize);
    }
}