using MedicalCentreApp.Data;
using MedicalCentreApp.Data.Models;
using MedicalCentreApp.Services.Core.Interfaces;
using MedicalCentreApp.ViewModels.Invoices;
using Microsoft.EntityFrameworkCore;

namespace MedicalCentreApp.Services.Core
{
    public class InvoiceService : IInvoiceService
    {
        private readonly MedicalCentreAppDbContext dbContext;

        public InvoiceService(MedicalCentreAppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<InvoiceDetailsViewModel?> GetDetailsAsync(int id)
        {
            InvoiceDetailsViewModel? invoice = await dbContext.Invoices
                .AsNoTracking()
                .Where(i => i.Id == id)
                .Select(i => new InvoiceDetailsViewModel
                {
                    Id = i.Id,
                    AppointmentId = i.AppointmentId,
                    Amount = i.Amount,
                    IssuedOn = i.IssuedOn,
                    IsPaid = i.IsPaid
                })
                .FirstOrDefaultAsync();

            return invoice;
        }

        public async Task CreateAsync(CreateInvoiceViewModel model)
        {
            Invoice invoice = new Invoice
            {                
                AppointmentId = model.AppointmentId,
                Amount = model.Amount,
                IssuedOn = DateTime.UtcNow,
                IsPaid = false
            };

            await dbContext.Invoices.AddAsync(invoice);
            await dbContext.SaveChangesAsync();
        }

        public async Task MarkAsPaidAsync(int id)
        {
            Invoice? invoice = await dbContext.Invoices
                .FirstOrDefaultAsync(i => i.Id == id);

            if (invoice == null)
                return;

            invoice.IsPaid = true;

            await dbContext.SaveChangesAsync();
        }
    }
}