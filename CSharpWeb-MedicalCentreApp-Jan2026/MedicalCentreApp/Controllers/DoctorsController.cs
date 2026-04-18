using MedicalCentreApp.Services.Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MedicalCentreApp.Controllers
{    
    public class DoctorsController : Controller
    {
        private readonly IDoctorService doctorService;
        private readonly IDepartmentService departmentService;

        public DoctorsController(IDoctorService doctorService, IDepartmentService departmentService)
        {
            this.doctorService = doctorService;
            this.departmentService = departmentService;
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> Index(string? specialty, int pageNumber = 1, int pageSize = 10)
        {
            var pagedDoctors = await doctorService
                .GetDoctorsWithPaginationAsync(specialty, pageNumber, pageSize);

            ViewData["CurrentFilter"] = specialty;

            return View(pagedDoctors);
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var doctor = await doctorService.GetDetailsAsync(id);

            if (doctor == null)
                return NotFound();

            return View(doctor);
        }
    }
}
