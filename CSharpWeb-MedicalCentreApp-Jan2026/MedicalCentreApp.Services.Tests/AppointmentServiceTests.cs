using MedicalCentreApp.Data.Models;
using MedicalCentreApp.Data.Models.Enums;
using MedicalCentreApp.Data.Repositories.Interfaces;
using MedicalCentreApp.Services.Core;
using MedicalCentreApp.ViewModels.Appointments;
using MedicalCentreApp.ViewModels.MedicalRecords;
using Moq;

namespace MedicalCentreApp.Services.Tests
{
    [TestFixture]
    public class AppointmentServiceTests
    {
        private Mock<IAppointmentRepository> appointmentRepositoryMock;
        private Mock<IPatientRepository> patientRepositoryMock;
        private Mock<IDoctorRepository> doctorRepositoryMock;
        private Mock<IMedicalRecordRepository> medicalRecordRepositoryMock;

        private AppointmentService appointmentService;

        [SetUp]
        public void Setup()
        {
            appointmentRepositoryMock = new Mock<IAppointmentRepository>();
            patientRepositoryMock = new Mock<IPatientRepository>();
            doctorRepositoryMock = new Mock<IDoctorRepository>();
            medicalRecordRepositoryMock = new Mock<IMedicalRecordRepository>();

            appointmentService = new AppointmentService(
                appointmentRepositoryMock.Object,
                patientRepositoryMock.Object,
                doctorRepositoryMock.Object,
                medicalRecordRepositoryMock.Object);
        }
        
        [Test]
        public async Task CreateAsync_InvalidTime_ReturnsFalse()
        {
            var model = new CreateAppointmentViewModel
            {
                Date = DateTime.Today,
                Time = TimeSpan.FromHours(7), 
                DoctorId = 1,
                PatientId = 1
            };

            var result = await appointmentService.CreateAsync(model);

            Assert.IsFalse(result.IsSuccess);
        }

        
        [Test]
        public async Task CreateMedicalRecordAsync_Valid_UpdatesAppointment()
        {
            var appointment = new Appointment
            {
                Id = 1,
                AppointmentStatus = AppointmentStatus.Scheduled
            };

            appointmentRepositoryMock
                .Setup(r => r.GetByIdAsync(1))
                .ReturnsAsync(appointment);

            var model = new CreateMedicalRecordViewModel
            {
                AppointmentId = 1,
                Diagnosis = "Test"
            };

            await appointmentService.CreateMedicalRecordAsync(model);

            medicalRecordRepositoryMock.Verify(r => r.AddAsync(It.IsAny<MedicalRecord>()), Times.Once);
            appointmentRepositoryMock.Verify(r => r.Update(It.IsAny<Appointment>()), Times.Once);

            Assert.AreEqual(AppointmentStatus.Completed, appointment.AppointmentStatus);
        }
    }
}