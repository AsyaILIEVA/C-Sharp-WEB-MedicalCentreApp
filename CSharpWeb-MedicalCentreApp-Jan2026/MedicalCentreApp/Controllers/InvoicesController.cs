using MedicalCentreApp.Services.Core.Interfaces;
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
    }
}