using MedicalCentreApp.Services.Core.Interfaces;
using MedicalCentreApp.ViewModels.Invoices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MedicalCentreApp.Areas.Administrator.Controllers
{
    [Area("Administrator")]
    [Authorize(Roles = "Administrator")]
    public class InvoicesManagementController : Controller
    {
        private readonly IInvoiceService invoiceService;

        public InvoicesManagementController(IInvoiceService invoiceService)
        {
            this.invoiceService = invoiceService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var invoices = await invoiceService.GetAllAsync();
            return View(invoices);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateInvoiceViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            await invoiceService.CreateAsync(model);

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Pay(int id)
        {
            await invoiceService.MarkAsPaidAsync(id);

            return RedirectToAction(nameof(Index));
        }
    }
}