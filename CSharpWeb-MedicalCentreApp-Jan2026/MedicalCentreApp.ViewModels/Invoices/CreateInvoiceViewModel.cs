using System.ComponentModel.DataAnnotations;
using static MedicalCentreApp.GCommon.ViewModelValidation.InvoiceViewModels;

namespace MedicalCentreApp.ViewModels.Invoices
{
    public class CreateInvoiceViewModel
    {
        public int AppointmentId { get; set; }

        [Required]
        [Range(InvoiceAmountMinValue, InvoiceAmountMaxValue)]
        public decimal Amount { get; set; }
    }
}
