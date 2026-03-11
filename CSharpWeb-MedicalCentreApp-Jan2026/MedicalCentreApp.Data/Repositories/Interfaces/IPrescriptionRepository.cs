using MedicalCentreApp.Data.Models;

namespace MedicalCentreApp.Data.Repositories.Interfaces
{
    public interface IPrescriptionRepository
    {
        IQueryable<Prescription> All();

        IQueryable<Prescription> AllAsNoTracking();

        Task<Prescription?> GetByIdAsync(int id);

        Task AddAsync(Prescription prescription);

        void Update(Prescription prescription);

        void Delete(Prescription prescription);

        Task<int> SaveChangesAsync();
    }
}