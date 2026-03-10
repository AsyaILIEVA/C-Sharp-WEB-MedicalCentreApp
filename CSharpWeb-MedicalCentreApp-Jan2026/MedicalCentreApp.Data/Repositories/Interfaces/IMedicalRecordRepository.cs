using MedicalCentreApp.Data.Models;

namespace MedicalCentreApp.Data.Repositories.Interfaces
{
    public interface IMedicalRecordRepository
    {
        IQueryable<MedicalRecord> All();

        IQueryable<MedicalRecord> AllAsNoTracking();

        Task<MedicalRecord?> GetByIdAsync(Guid id);

        Task AddAsync(MedicalRecord record);

        void Update(MedicalRecord record);

        void Delete(MedicalRecord record);

        Task<int> SaveChangesAsync();
    }
}