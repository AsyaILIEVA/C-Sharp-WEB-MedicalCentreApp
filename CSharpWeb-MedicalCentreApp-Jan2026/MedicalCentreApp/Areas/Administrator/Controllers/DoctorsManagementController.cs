using MedicalCentreApp.Services.Core.Interfaces;
using MedicalCentreApp.ViewModels.Doctors;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace MedicalCentreApp.Areas.Administrator.Controllers
{
    [Area("Administrator")]
    [Authorize(Roles = "Administrator")]
    public class DoctorsManagementController : Controller
    {
        private readonly IDoctorService doctorService;
        private readonly IDepartmentService departmentService;

        public DoctorsManagementController(
            IDoctorService doctorService,
            IDepartmentService departmentService)
        {
            this.doctorService = doctorService;
            this.departmentService = departmentService;
        }
                
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var departments = await departmentService.GetAllAsync();

            var model = new CreateDoctorInputModel
            {
                Departments = departments
                    .Select(d => new SelectListItem
                {
                    Value = d.Id.ToString(),
                    Text = d.Name
                })
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateDoctorInputModel model)
        {
            if (!ModelState.IsValid)
            {
                await PopulateDepartments(model);
                return View(model);
            }

            try
            {
                await doctorService.CreateAsync(model);
            }
            catch (InvalidOperationException ex)
            {
                ModelState.AddModelError("", ex.Message);
                await PopulateDepartments(model);
                return View(model);
            }

            return RedirectToAction("Index", "Doctors", new { area = "" });
        }
        
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var model = await doctorService.GetForEditAsync(id);

            if (model == null)
                return NotFound();

            await PopulateDepartments(model);

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(EditDoctorInputModel model)
        {
            if (!ModelState.IsValid)
            {
                await PopulateDepartments(model);
                return View(model);
            }

            var success = await doctorService.UpdateAsync(model);

            if (!success)
                return NotFound();

            return RedirectToAction("Index", "Doctors", new { area = "" });
        }
        
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var doctor = await doctorService.GetForDeleteAsync(id);

            if (doctor == null)
                return NotFound();

            return View(doctor);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var success = await doctorService.DeleteAsync(id);

            if (!success)
                return BadRequest("Doctor has appointments and cannot be deleted.");

            return RedirectToAction("Index", "Doctors", new { area = "" });
        }

        private async Task PopulateDepartments(CreateDoctorInputModel model)
        {
            var departments = await departmentService.GetAllAsync();

            model.Departments = departments
                .Select(d => new SelectListItem
            {
                Value = d.Id.ToString(),
                Text = d.Name
            });
        }

        private async Task PopulateDepartments(EditDoctorInputModel model)
        {
            var departments = await departmentService.GetAllAsync();

            model.Departments = departments
                .Select(d => new SelectListItem
            {
                Value = d.Id.ToString(),
                Text = d.Name,
                Selected = d.Id == model.DepartmentId
            });
        }
    }
}