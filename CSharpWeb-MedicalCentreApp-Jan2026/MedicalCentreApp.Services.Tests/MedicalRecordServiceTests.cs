using MedicalCentreApp.Data.Models;
using MedicalCentreApp.Data.Repositories.Interfaces;
using MedicalCentreApp.Services.Core;
using MedicalCentreApp.ViewModels.MedicalRecords;
using Moq;


namespace MedicalCentreApp.Services.Tests
{
    [TestFixture]
    public class MedicalRecordServiceTests
    {
        private Mock<IMedicalRecordRepository> medicalRecordRepositoryMock;
        private MedicalRecordService medicalRecordService;

        [SetUp]
        public void Setup()
        {
            medicalRecordRepositoryMock = new Mock<IMedicalRecordRepository>();
            medicalRecordService = new MedicalRecordService(medicalRecordRepositoryMock.Object);
        }
                
        [Test]
        public async Task CreateAsync_ShouldAddRecordAndReturnId()
        {
            var model = new CreateMedicalRecordViewModel
            {
                AppointmentId = 1,
                Diagnosis = "Test diagnosis"
            };

            Guid result = await medicalRecordService.CreateAsync(model);

            Assert.AreNotEqual(Guid.Empty, result);

            medicalRecordRepositoryMock.Verify(r => r.AddAsync(It.IsAny<MedicalRecord>()), Times.Once);
            medicalRecordRepositoryMock.Verify(r => r.SaveChangesAsync(), Times.Once);
        }        
    }
}