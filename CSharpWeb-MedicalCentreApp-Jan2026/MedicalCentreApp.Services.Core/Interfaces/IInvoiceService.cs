using MedicalCentreApp.ViewModels.Invoices;

namespace MedicalCentreApp.Services.Core.Interfaces
{
    public interface IInvoiceService
    {
        Task<InvoiceDetailsViewModel?> GetDetailsAsync(int id);

        Task CreateAsync(CreateInvoiceViewModel model);

        Task MarkAsPaidAsync(int id);
    }
}