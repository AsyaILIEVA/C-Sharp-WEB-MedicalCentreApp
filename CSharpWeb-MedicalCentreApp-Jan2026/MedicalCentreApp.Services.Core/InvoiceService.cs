using MedicalCentreApp.Data.Models;
using MedicalCentreApp.Data.Repositories.Interfaces;
using MedicalCentreApp.Services.Core.Interfaces;
using MedicalCentreApp.ViewModels.Invoices;
using Microsoft.EntityFrameworkCore;

namespace MedicalCentreApp.Services.Core
{
    public class InvoiceService : IInvoiceService
    {
        private readonly IInvoiceRepository invoiceRepository;

        public InvoiceService(IInvoiceRepository invoiceRepository)
        {
            this.invoiceRepository = invoiceRepository;
        }

        public async Task<IEnumerable<InvoiceListViewModel>> GetAllAsync()
        {
            return await invoiceRepository
                .AllAsNoTracking()
                .Select(i => new InvoiceListViewModel
                {
                    Id = i.Id,
                    Amount = i.Amount,
                    IsPaid = i.IsPaid,
                    IssuedOn = i.IssuedOn,
                    AppointmentId = i.AppointmentId
                })
                .ToListAsync();
        }
        public async Task<InvoiceDetailsViewModel?> GetDetailsAsync(int id)
        {
            InvoiceDetailsViewModel? invoice = await invoiceRepository
                .AllAsNoTracking()
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

            await invoiceRepository.AddAsync(invoice);

            await invoiceRepository.SaveChangesAsync();
        }

        public async Task MarkAsPaidAsync(int id)
        {
            Invoice? invoice = await invoiceRepository.GetByIdAsync(id);

            if (invoice == null)
            {
                return;
            }

            invoice.IsPaid = true;

            invoiceRepository.Update(invoice);

            await invoiceRepository.SaveChangesAsync();
        }
    }
}