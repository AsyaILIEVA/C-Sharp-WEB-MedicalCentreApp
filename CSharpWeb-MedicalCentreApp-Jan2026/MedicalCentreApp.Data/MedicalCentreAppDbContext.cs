using MedicalCentreApp.Data.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace MedicalCentreApp.Data
{
    public class MedicalCentreAppDbContext : IdentityDbContext<ApplicationUser>
    {
        public MedicalCentreAppDbContext(DbContextOptions<MedicalCentreAppDbContext> dbContextOptions)
                    : base(dbContextOptions)
        {

        }

        public virtual DbSet<Patient> Patients { get; set; } = null!;
        public virtual DbSet<Doctor> Doctors { get; set; } = null!;
        public virtual DbSet<Department> Departments { get; set; } = null!;

        public virtual DbSet<Appointment> Appointments { get; set; } = null!;
        public virtual DbSet<MedicalRecord> MedicalRecords { get; set; } = null!;
        
        public virtual DbSet<Invoice> Invoices { get; set; } = null!;
        public virtual DbSet<Prescription> Prescriptions { get; set; } = null!;


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly
                (typeof(MedicalCentreAppDbContext).Assembly);

            modelBuilder.Entity<Doctor>()
                .HasIndex(d => new { d.FullName, d.Specialty, d.DepartmentId })
                .IsUnique();

            modelBuilder.Entity<Patient>()
                .HasIndex(p => p.EGN)
                .IsUnique();

            modelBuilder.Entity<Appointment>()
                .HasOne(a => a.MedicalRecord)
                .WithOne(m => m.Appointment)
                .HasForeignKey<MedicalRecord>(m => m.AppointmentId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Appointment>()
                .HasOne(a => a.Invoice)
                .WithOne(i => i.Appointment)
                .HasForeignKey<Invoice>(i => i.AppointmentId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
