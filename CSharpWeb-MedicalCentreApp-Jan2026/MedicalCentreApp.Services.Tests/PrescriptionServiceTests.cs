using MedicalCentreApp.Data.Models;
using MedicalCentreApp.Data.Repositories.Interfaces;
using MedicalCentreApp.Services.Core;
using MedicalCentreApp.ViewModels.Prescriptions;
using Moq;


namespace MedicalCentreApp.Services.Tests
{
    [TestFixture]
    public class PrescriptionServiceTests
    {
        private Mock<IPrescriptionRepository> prescriptionRepositoryMock;
        private PrescriptionService prescriptionService;

        [SetUp]
        public void Setup()
        {
            prescriptionRepositoryMock = new Mock<IPrescriptionRepository>();
            prescriptionService = new PrescriptionService(prescriptionRepositoryMock.Object);
        }

        
        [Test]
        public async Task CreateAsync_ShouldAddPrescription()
        {
            var model = new CreatePrescriptionViewModel
            {
                MedicationName = "TestMed",
                Dosage = "1x daily",
                MedicalRecordId = Guid.NewGuid(),
                ExpirationDate = DateTime.UtcNow.AddDays(5)
            };

            await prescriptionService.CreateAsync(model);

            prescriptionRepositoryMock.Verify(r => r.AddAsync(It.IsAny<Prescription>()), Times.Once);
            prescriptionRepositoryMock.Verify(r => r.SaveChangesAsync(), Times.Once);
        }

        
        [Test]
        public async Task CreateAsync_ShouldMapCorrectData()
        {
            Prescription captured = null;

            prescriptionRepositoryMock
                .Setup(r => r.AddAsync(It.IsAny<Prescription>()))
                .Callback<Prescription>(p => captured = p);

            var model = new CreatePrescriptionViewModel
            {
                MedicationName = "Paracetamol",
                Dosage = "2x daily",
                MedicalRecordId = Guid.NewGuid(),
                ExpirationDate = DateTime.UtcNow.AddDays(10)
            };

            await prescriptionService.CreateAsync(model);

            Assert.IsNotNull(captured);
            Assert.AreEqual("Paracetamol", captured.MedicationName);
            Assert.AreEqual("2x daily", captured.Dosage);
            Assert.AreEqual(model.MedicalRecordId, captured.MedicalRecordId);
        }
    }
}