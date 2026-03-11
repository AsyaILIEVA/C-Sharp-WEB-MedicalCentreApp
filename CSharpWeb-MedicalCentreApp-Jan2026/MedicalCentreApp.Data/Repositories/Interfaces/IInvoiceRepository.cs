using MedicalCentreApp.Data.Models;

namespace MedicalCentreApp.Data.Repositories.Interfaces
{
    public interface IInvoiceRepository
    {
        IQueryable<Invoice> All();

        IQueryable<Invoice> AllAsNoTracking();

        Task<Invoice?> GetByIdAsync(int id);

        Task AddAsync(Invoice invoice);

        void Update(Invoice invoice);

        void Delete(Invoice invoice);

        Task<int> SaveChangesAsync();
    }
}