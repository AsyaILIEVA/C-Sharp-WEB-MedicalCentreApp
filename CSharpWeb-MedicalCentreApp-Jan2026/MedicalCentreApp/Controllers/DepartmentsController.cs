using MedicalCentreApp.Services.Core.Interfaces;
using MedicalCentreApp.ViewModels.Departments;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MedicalCentreApp.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class DepartmentsController : Controller
    {
        private readonly IDepartmentService departmentService;

        public DepartmentsController(IDepartmentService departmentService)
        {
            this.departmentService = departmentService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var departments = await departmentService.GetAllAsync();
            return View(departments);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View(new CreateDepartmentViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateDepartmentViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            await departmentService.CreateAsync(model);

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            bool exists = await departmentService.ExistsAsync(id);

            if (!exists)
                return NotFound();

            var department = await departmentService.GetForDeleteAsync(id);

            return View(department);
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            bool exists = await departmentService.ExistsAsync(id);

            if (!exists)
                return NotFound();

            await departmentService.DeleteAsync(id);

            return RedirectToAction(nameof(Index));
        }


    }
}