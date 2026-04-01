using MedicalCentreApp.Services.Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MedicalCentreApp.Areas.Administrator.Controllers
{
    [Area("Administrator")]
    [Authorize(Roles = "Administrator")]
    public class DashboardController : Controller
    {
        private readonly IDoctorService doctorService;
        private readonly IPatientService patientService;
        private readonly IAppointmentService appointmentService;
        private readonly IDepartmentService departmentService;

        public DashboardController(
            IDoctorService doctorService,
            IPatientService patientService,
            IAppointmentService appointmentService,
            IDepartmentService departmentService)
        {
            this.doctorService = doctorService;
            this.patientService = patientService;
            this.appointmentService = appointmentService;
            this.departmentService = departmentService;
        }

        public async Task<IActionResult> Index()
        {
            ViewBag.DoctorsCount = (await doctorService.GetAllAsync(null)).Count();
            ViewBag.PatientsCount = (await patientService.GetAllAsync()).Count();
            ViewBag.AppointmentsCount = (await appointmentService.GetAllAsync()).Count();
            ViewBag.DepartmentsCount = (await departmentService.GetAllAsync()).Count();

            return View();
        }
    }
}
