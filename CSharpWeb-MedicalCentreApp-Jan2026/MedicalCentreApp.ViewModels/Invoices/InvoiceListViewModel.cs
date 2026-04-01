namespace MedicalCentreApp.ViewModels.Invoices
{
    public class InvoiceListViewModel
    {
        public int Id { get; set; }

        public decimal Amount { get; set; }

        public bool IsPaid { get; set; }

        public DateTime IssuedOn { get; set; }

        public int AppointmentId { get; set; }
    }
}