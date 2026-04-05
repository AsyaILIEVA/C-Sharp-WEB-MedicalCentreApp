using MedicalCentreApp.Data.Models;
using MedicalCentreApp.Data.Repositories.Interfaces;
using MedicalCentreApp.Services.Core;
using MedicalCentreApp.ViewModels.Doctors;
using Moq;

namespace MedicalCentreApp.Services.Tests
{
    [TestFixture]
    public class DoctorServiceTests
    {
        private Mock<IDoctorRepository> doctorRepositoryMock;
        private DoctorService doctorService;

        [SetUp]
        public void Setup()
        {
            doctorRepositoryMock = new Mock<IDoctorRepository>();
            doctorService = new DoctorService(doctorRepositoryMock.Object);
        }
                
        [Test]
        public async Task CreateAsync_WithoutImage_ShouldAddDoctor()
        {
            var model = new CreateDoctorInputModel
            {
                FullName = "Dr. Test",
                Specialty = "Cardiology",
                Image = null
            };

            await doctorService.CreateAsync(model);

            doctorRepositoryMock.Verify(r => r.AddAsync(It.IsAny<Doctor>()), Times.Once);
            doctorRepositoryMock.Verify(r => r.SaveChangesAsync(), Times.Once);
        }
        
        [Test]
        public async Task GetForEditAsync_NotFound_ReturnsNull()
        {
            doctorRepositoryMock
                .Setup(r => r.GetByIdAsync(1))
                .ReturnsAsync((Doctor?)null);

            var result = await doctorService.GetForEditAsync(1);

            Assert.IsNull(result);
        }

        [Test]
        public async Task GetForEditAsync_Found_ReturnsModel()
        {
            var doctor = new Doctor
            {
                Id = 1,
                FullName = "Dr. Test",
                Specialty = "Cardiology",
                ImageUrl = "img.jpg"
            };

            doctorRepositoryMock
                .Setup(r => r.GetByIdAsync(1))
                .ReturnsAsync(doctor);

            var result = await doctorService.GetForEditAsync(1);

            Assert.IsNotNull(result);
            Assert.AreEqual("Dr. Test", result.FullName);
        }

        [Test]
        public async Task UpdateAsync_NotFound_ReturnsFalse()
        {
            doctorRepositoryMock
                .Setup(r => r.GetByIdAsync(1))
                .ReturnsAsync((Doctor?)null);

            var model = new EditDoctorInputModel { Id = 1 };

            var result = await doctorService.UpdateAsync(model);

            Assert.IsFalse(result);
        }

        [Test]
        public async Task UpdateAsync_Valid_NoImage_ReturnsTrue()
        {
            var doctor = new Doctor { Id = 1 };

            doctorRepositoryMock
                .Setup(r => r.GetByIdAsync(1))
                .ReturnsAsync(doctor);

            var model = new EditDoctorInputModel
            {
                Id = 1,
                FullName = "Updated",
                Specialty = "Neurology",
                Image = null
            };

            var result = await doctorService.UpdateAsync(model);

            Assert.IsTrue(result);
            doctorRepositoryMock.Verify(r => r.Update(doctor), Times.Once);
            doctorRepositoryMock.Verify(r => r.SaveChangesAsync(), Times.Once);
        }

        [Test]
        public async Task DeleteAsync_NotFound_ReturnsFalse()
        {
            doctorRepositoryMock
                .Setup(r => r.GetDoctorWithAppointmentsAsync(1))
                .ReturnsAsync((Doctor?)null);

            var result = await doctorService.DeleteAsync(1);

            Assert.IsFalse(result);
        }
                
        [Test]
        public async Task DeleteAsync_WithAppointments_ReturnsFalse()
        {
            var doctor = new Doctor
            {
                Id = 1,
                Appointments = new List<Appointment> { new Appointment() }
            };

            doctorRepositoryMock
                .Setup(r => r.GetDoctorWithAppointmentsAsync(1))
                .ReturnsAsync(doctor);

            var result = await doctorService.DeleteAsync(1);

            Assert.IsFalse(result);
        }

        [Test]
        public async Task DeleteAsync_NoAppointments_ReturnsTrue()
        {
            var doctor = new Doctor
            {
                Id = 1,
                Appointments = new List<Appointment>()
            };

            doctorRepositoryMock
                .Setup(r => r.GetDoctorWithAppointmentsAsync(1))
                .ReturnsAsync(doctor);

            var result = await doctorService.DeleteAsync(1);

            Assert.IsTrue(result);
            doctorRepositoryMock.Verify(r => r.Delete(doctor), Times.Once);
            doctorRepositoryMock.Verify(r => r.SaveChangesAsync(), Times.Once);
        }
    }
}