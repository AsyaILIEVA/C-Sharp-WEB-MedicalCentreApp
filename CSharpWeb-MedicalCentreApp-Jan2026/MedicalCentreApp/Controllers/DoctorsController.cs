using MedicalCentreApp.Services.Core;
using MedicalCentreApp.Services.Core.Interfaces;
using MedicalCentreApp.ViewModels.Doctors;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

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

        [Authorize(Roles = "Administrator")]
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var departments = await departmentService.GetAllAsync();

            var model = new CreateDoctorInputModel
            {
                Departments = departments.Select(d => new SelectListItem
                {
                    Value = d.Id.ToString(),
                    Text = d.Name
                })
            };

            return View(model);
        }

        [Authorize(Roles = "Administrator")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateDoctorInputModel model)
        {
            if (!ModelState.IsValid)
            {
                var departments = await departmentService.GetAllAsync();

                model.Departments = departments.Select(d => new SelectListItem
                {
                    Value = d.Id.ToString(),
                    Text = d.Name
                });

                return View(model);
            }

            try
            {
                await doctorService.CreateAsync(model);
            }
            catch (InvalidOperationException ex)
            {
                ModelState.AddModelError("", ex.Message);

                var departments = await departmentService.GetAllAsync();
                model.Departments = departments.Select(d => new SelectListItem
                {
                    Value = d.Id.ToString(),
                    Text = d.Name
                });

                return View(model);
            }

            return RedirectToAction(nameof(Index));
        }

        [Authorize(Roles = "Administrator")]
        [HttpGet]        
        public async Task<IActionResult> Edit(int id)
        {
            var model = await doctorService.GetForEditAsync(id);

            if (model == null)
                return NotFound();

            var departments = await departmentService.GetAllAsync();

            model.Departments = departments.Select(d => new SelectListItem
            {
                Value = d.Id.ToString(),
                Text = d.Name,
                Selected = d.Id == model.DepartmentId
            });

            return View(model);
        }

        [Authorize(Roles = "Administrator")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(EditDoctorInputModel model)
        {
            if (!ModelState.IsValid)
            {
                var departments = await departmentService.GetAllAsync();

                model.Departments = departments.Select(d => new SelectListItem
                {
                    Value = d.Id.ToString(),
                    Text = d.Name,
                    Selected = d.Id == model.DepartmentId
                });

                return View(model);
            }


            var success = await doctorService.UpdateAsync(model);

            if (!success)
                return NotFound();

            return RedirectToAction(nameof(Index));
        }

        [Authorize(Roles = "Administrator")]
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var doctor = await doctorService.GetForDeleteAsync(id);

            if (doctor == null)
                return NotFound();

            return View(doctor);
        }

        [Authorize(Roles = "Administrator")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var success = await doctorService.DeleteAsync(id);

            if (!success)
                return BadRequest("Doctor has appointments and cannot be deleted.");

            return RedirectToAction(nameof(Index));
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
