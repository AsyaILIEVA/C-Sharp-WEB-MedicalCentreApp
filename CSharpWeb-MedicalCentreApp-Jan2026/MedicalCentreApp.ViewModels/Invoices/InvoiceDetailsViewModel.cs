namespace MedicalCentreApp.ViewModels.Invoices
{
    public class InvoiceDetailsViewModel
    {
        public int Id { get; set; }

        public int AppointmentId { get; set; }

        public decimal Amount { get; set; }

        public DateTime IssuedOn { get; set; }

        public bool IsPaid { get; set; }
    }

}
