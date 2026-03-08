using MedicalCentreApp.Services.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace MedicalCentreApp.Controllers
{
    public class InvoicesController : Controller
    {
        private readonly IInvoiceService invoiceService;

        public InvoicesController(IInvoiceService invoiceService)
        {
            this.invoiceService = invoiceService;
        }

        public async Task<IActionResult> Details(int id)
        {
            var invoice = await invoiceService.GetDetailsAsync(id);

            if (invoice == null)
                return NotFound();

            return View(invoice);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Pay(int id)
        {
            await invoiceService.MarkAsPaidAsync(id);

            return RedirectToAction(nameof(Details), new { id });
        }
    }
}