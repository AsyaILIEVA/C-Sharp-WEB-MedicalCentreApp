using MedicalCentreApp.ViewModels.Invoices;

namespace MedicalCentreApp.Services.Core.Interfaces
{
    public interface IInvoiceService
    {
        Task<IEnumerable<InvoiceListViewModel>> GetAllAsync();
        
        Task<InvoiceDetailsViewModel?> GetDetailsAsync(int id);

        Task CreateAsync(CreateInvoiceViewModel model);

        Task MarkAsPaidAsync(int id);
    }
}