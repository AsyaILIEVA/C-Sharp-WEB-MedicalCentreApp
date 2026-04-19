using MedicalCentreApp.Data.Models;
using MedicalCentreApp.Data.Repositories.Interfaces;
using MedicalCentreApp.Services.Core;

using MedicalCentreApp.ViewModels.Patients;
using Moq;

namespace MedicalCentreApp.Services.Tests
{
    [TestFixture]
    public class PatientServiceTests
    {
        private Mock<IPatientRepository> patientRepositoryMock;
        private PatientService patientService;
        private Patient samplePatient;

        [SetUp]
        public void Setup()
        {
            patientRepositoryMock = new Mock<IPatientRepository>();
            
            var doctor = new Doctor
            {
                Id = 1,
                FullName = "Dr. Smith",
                Specialty = "Cardiology"
            };
            
            samplePatient = new Patient
            {
                Id = 1,
                FirstName = "John",
                MiddleName = "M",
                LastName = "Doe",
                EGN = "1234567890",
                DateOfBirth = new DateTime(1990, 1, 1),
                PhoneNumber = "555-1234",
                Email = "john@example.com",
                Address = "123 Main St",
                UserId = "user-1",
                Appointments = new List<Appointment>
                {
                    new Appointment
                    {
                        Id = 1,
                        Date = DateTime.Now.AddDays(1),
                        Doctor = doctor,
                        Reason = "Checkup",
                        MedicalRecord = new MedicalRecord
                        {
                            Diagnosis = "Healthy",
                            Prescriptions = new List<Prescription>
                            {
                                new Prescription { MedicationName = "Vitamin D", Dosage = "1000 IU" }
                            }
                        }
                    },
                    new Appointment
                    {
                        Id = 2,
                        Date = DateTime.Now.AddDays(-1), 
                        Doctor = doctor,
                        Reason = "Follow-up",
                        MedicalRecord = null
                    }
                }
            };

            var patients = new List<Patient> { samplePatient }.AsQueryable();

          
            patientRepositoryMock.Setup(r => r.AllAsNoTracking()).Returns(patients);

           
            patientRepositoryMock.Setup(r => r.All()).Returns(patients);

           
            patientRepositoryMock.Setup(r => r.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync((int id) => id == 1 ? samplePatient : null);

            //patientService = new PatientService(patientRepositoryMock.Object);
        }

        
        [Test]
        public async Task CreateAsync_AddsPatient()
        {
            var model = new CreateEditPatientViewModel
            {
                FirstName = "Jane",
                MiddleName = "A",
                LastName = "Doe",
                EGN = "0987654321",
                DateOfBirth = new DateTime(1995, 1, 1),
                PhoneNumber = "555-9876",
                Email = "jane@example.com",
                Address = "456 Elm St"
            };

            await patientService.CreateAsync(model);

            patientRepositoryMock.Verify(r => r.AddAsync(It.IsAny<Patient>()), Times.Once);
            patientRepositoryMock.Verify(r => r.SaveChangesAsync(), Times.Once);
        }

        [Test]
        public async Task CreateFromUserAsync_AddsPatient()
        {
            string userId = "user-2";
            string email = "user2@example.com";

            await patientService.CreateFromUserAsync(userId, email);

            patientRepositoryMock.Verify(r => r.AddAsync(It.Is<Patient>(
                p => p.UserId == userId && p.Email == email)), Times.Once);
            patientRepositoryMock.Verify(r => r.SaveChangesAsync(), Times.Once);
        }

        [Test]
        public async Task UpdateAsync_ValidPatient_ReturnsTrue()
        {
            var model = new CreateEditPatientViewModel
            {
                Id = 1,
                FirstName = "Updated John",
                MiddleName = "M",
                LastName = "Doe",
                EGN = "1234567890",
                DateOfBirth = samplePatient.DateOfBirth,
                PhoneNumber = "555-1234",
                Email = "john@example.com",
                Address = "123 Main St"
            };

            var result = await patientService.UpdateAsync(1, model);
            Assert.IsTrue(result);
            patientRepositoryMock.Verify(r => r.Update(It.IsAny<Patient>()), Times.Once);
            patientRepositoryMock.Verify(r => r.SaveChangesAsync(), Times.Once);
        }

        [Test]
        public async Task UpdateAsync_InvalidId_ReturnsFalse()
        {
            var model = new CreateEditPatientViewModel { Id = 2 };
            var result = await patientService.UpdateAsync(2, model);
            Assert.IsFalse(result);
        }

        [Test]
        public async Task DeleteAsync_ValidId_ReturnsTrue()
        {
            var result = await patientService.DeleteAsync(1);
            Assert.IsTrue(result);
            patientRepositoryMock.Verify(r => r.Delete(It.IsAny<Patient>()), Times.Once);
            patientRepositoryMock.Verify(r => r.SaveChangesAsync(), Times.Once);
        }

        [Test]
        public async Task DeleteAsync_InvalidId_ReturnsFalse()
        {
            var result = await patientService.DeleteAsync(999);
            Assert.IsFalse(result);
        }       
    }
}