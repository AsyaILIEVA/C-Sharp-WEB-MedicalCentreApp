using MedicalCentreApp.Services.Core.Interfaces;
using MedicalCentreApp.ViewModels.Invoices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MedicalCentreApp.Controllers
{
    [Authorize]
    public class InvoicesController : Controller
    {
        private readonly IInvoiceService invoiceService;

        public InvoicesController(IInvoiceService invoiceService)
        {
            this.invoiceService = invoiceService;
        }

        [HttpGet]
        [Authorize(Roles = "Administrator,Doctor,Patient")]
        public async Task<IActionResult> Index()
        {
            var invoices = await invoiceService.GetAllAsync();
            return View(invoices);
        }

        [HttpGet]
        [Authorize(Roles = "Administrator")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Administrator")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateInvoiceViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            await invoiceService.CreateAsync(model);

            return RedirectToAction(nameof(Index));
        }

        [Authorize(Roles = "Administrator,Doctor,Patient")]
        public async Task<IActionResult> Details(int id)
        {
            var invoice = await invoiceService.GetDetailsAsync(id);

            if (invoice == null)
                return NotFound();
            
            if (User.IsInRole("Patient"))
            {
                var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;

                if (!invoice.PatientUserId.Equals(userId))
                {
                    return Forbid();
                }
            }

            return View(invoice);
        }
            

        [HttpPost]
        [Authorize(Roles = "Administrator")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Pay(int id)
        {
            await invoiceService.MarkAsPaidAsync(id);

            return RedirectToAction(nameof(Details), new { id });
        }
    }
}