using MedicalCentreApp.Data.Repositories;
using MedicalCentreApp.Data.Repositories.Interfaces;
using MedicalCentreApp.Services.Core;
using MedicalCentreApp.Services.Core.Interfaces;

namespace MedicalCentreApp.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<IDoctorService, DoctorService>();
            services.AddScoped<IDoctorRepository, DoctorRepository>();

            services.AddScoped<IPatientService, PatientService>();
            services.AddScoped<IPatientRepository, PatientRepository>();

            services.AddScoped<IAppointmentService, AppointmentService>();
            services.AddScoped<IAppointmentRepository, AppointmentRepository>();

            services.AddScoped<IPrescriptionService, PrescriptionService>();
            services.AddScoped<IPrescriptionRepository, PrescriptionRepository>();

            services.AddScoped<IInvoiceService, InvoiceService>();
            services.AddScoped<IInvoiceRepository, InvoiceRepository>();

            services.AddScoped<IDepartmentService, DepartmentService>();
            services.AddScoped<IDepartmentRepository, DepartmentRepository>();

            services.AddScoped<IMedicalRecordService, MedicalRecordService>();
            services.AddScoped<IMedicalRecordRepository, MedicalRecordRepository>();

            return services;
        }
    }
}
