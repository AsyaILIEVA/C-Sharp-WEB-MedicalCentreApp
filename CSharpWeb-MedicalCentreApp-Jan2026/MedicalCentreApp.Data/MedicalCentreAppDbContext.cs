using MedicalCentreApp.Data.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace MedicalCentreApp.Data
{
    public class MedicalCentreAppDbContext : IdentityDbContext
    {
        public MedicalCentreAppDbContext(DbContextOptions<MedicalCentreAppDbContext> dbContextOptions)
                    : base(dbContextOptions)
        {

        }

        public virtual DbSet<Patient> Patients { get; set; }
        public virtual DbSet<Doctor> Doctors { get; set; }
        public virtual DbSet<Appointment> Appointments { get; set; }
        public virtual DbSet<MedicalRecord> MedicalRecords { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly
                (typeof(MedicalCentreAppDbContext).Assembly);
        }
    }
}
