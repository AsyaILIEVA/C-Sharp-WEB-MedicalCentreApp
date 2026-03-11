using MedicalCentreApp.Data.Models;
using MedicalCentreApp.Data.Repositories.Interfaces;

namespace MedicalCentreApp.Data.Repositories
{
    public class InvoiceRepository : BaseRepository<Invoice>, IInvoiceRepository
    {
        public InvoiceRepository(MedicalCentreAppDbContext dbContext)
            : base(dbContext)
        {
        }

        public async Task<Invoice?> GetByIdAsync(int id)
        {
            Invoice? invoice = await this.dbSet.FindAsync(id);

            return invoice;
        }
    }
}