using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static MedicalCentreApp.Data.Common.EntityValidation.Invoice;

namespace MedicalCentreApp.Data.Models
{
    public class Invoice
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Column(TypeName = InvoiceAmountColumnType)]
        public decimal Amount { get; set; }

        public bool IsPaid { get; set; } = false;

        [Required]
        public DateTime IssuedOn { get; set; } = DateTime.UtcNow;

        [ForeignKey(nameof(Appointment))]
        public int AppointmentId { get; set; }

        public virtual Appointment Appointment { get; set; } = null!;
    }
}
