using MedicalCentreApp.Data.Models;

namespace MedicalCentreApp.Data.Repositories.Interfaces
{
    public interface IPatientRepository
    {
        Task<(IEnumerable<Patient> Patients, int TotalCount)> GetPagedAsync(int page, int pageSize);
        IQueryable<Patient> All();

        IQueryable<Patient> AllAsNoTracking();

        Task<Patient?> GetByIdAsync(int id);

        Task AddAsync(Patient patient);

        void Update(Patient patient);

        void Delete(Patient patient);

        Task<int> SaveChangesAsync();
    }
}