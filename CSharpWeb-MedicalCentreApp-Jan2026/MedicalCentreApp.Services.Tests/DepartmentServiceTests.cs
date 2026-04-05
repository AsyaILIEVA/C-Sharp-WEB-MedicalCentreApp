using MedicalCentreApp.Data.Models;
using MedicalCentreApp.Data.Repositories.Interfaces;
using MedicalCentreApp.Services.Core;
using MedicalCentreApp.ViewModels.Departments;
using Moq;


namespace MedicalCentreApp.Services.Tests
{
    [TestFixture]
    public class DepartmentServiceTests
    {
        private Mock<IDepartmentRepository> departmentRepositoryMock;
        private DepartmentService departmentService;

        [SetUp]
        public void Setup()
        {
            departmentRepositoryMock = new Mock<IDepartmentRepository>();
            departmentService = new DepartmentService(departmentRepositoryMock.Object);
        }

        
        [Test]
        public async Task CreateAsync_ShouldAddDepartment()
        {
            var model = new CreateDepartmentViewModel
            {
                Name = "Cardiology"
            };

            await departmentService.CreateAsync(model);

            departmentRepositoryMock.Verify(r => r.AddAsync(It.IsAny<Department>()), Times.Once);
            departmentRepositoryMock.Verify(r => r.SaveChangesAsync(), Times.Once);
        }

        
        [Test]
        public async Task CreateAsync_ShouldMapCorrectData()
        {
            Department captured = null;

            departmentRepositoryMock
                .Setup(r => r.AddAsync(It.IsAny<Department>()))
                .Callback<Department>(d => captured = d);

            var model = new CreateDepartmentViewModel
            {
                Name = "Neurology"
            };

            await departmentService.CreateAsync(model);

            Assert.IsNotNull(captured);
            Assert.AreEqual("Neurology", captured.Name);
        }

        
        [Test]
        public async Task DeleteAsync_DepartmentExists_ShouldDelete()
        {
            var department = new Department { Id = 1, Name = "Test" };

            departmentRepositoryMock
                .Setup(r => r.GetByIdAsync(1))
                .ReturnsAsync(department);

            await departmentService.DeleteAsync(1);

            departmentRepositoryMock.Verify(r => r.Delete(department), Times.Once);
            departmentRepositoryMock.Verify(r => r.SaveChangesAsync(), Times.Once);
        }

        
        [Test]
        public async Task DeleteAsync_DepartmentNotFound_ShouldDoNothing()
        {
            departmentRepositoryMock
                .Setup(r => r.GetByIdAsync(1))
                .ReturnsAsync((Department?)null);

            await departmentService.DeleteAsync(1);

            departmentRepositoryMock.Verify(r => r.Delete(It.IsAny<Department>()), Times.Never);
            departmentRepositoryMock.Verify(r => r.SaveChangesAsync(), Times.Never);
        }
    }
}