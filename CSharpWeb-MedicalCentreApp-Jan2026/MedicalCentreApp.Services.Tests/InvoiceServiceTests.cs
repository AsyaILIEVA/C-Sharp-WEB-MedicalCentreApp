using MedicalCentreApp.Data.Models;
using MedicalCentreApp.Data.Repositories.Interfaces;
using MedicalCentreApp.Services.Core;
using MedicalCentreApp.ViewModels.Invoices;
using Moq;


namespace MedicalCentreApp.Services.Tests
{
    [TestFixture]
    public class InvoiceServiceTests
    {
        private Mock<IInvoiceRepository> invoiceRepositoryMock;
        private InvoiceService invoiceService;

        [SetUp]
        public void Setup()
        {
            invoiceRepositoryMock = new Mock<IInvoiceRepository>();
            invoiceService = new InvoiceService(invoiceRepositoryMock.Object);
        }

       
        [Test]
        public async Task CreateAsync_ShouldAddInvoice()
        {
            var model = new CreateInvoiceViewModel
            {
                AppointmentId = 1,
                Amount = 150
            };

            await invoiceService.CreateAsync(model);

            invoiceRepositoryMock.Verify(r => r.AddAsync(It.IsAny<Invoice>()), Times.Once);
            invoiceRepositoryMock.Verify(r => r.SaveChangesAsync(), Times.Once);
        }

        
        [Test]
        public async Task CreateAsync_ShouldMapCorrectData()
        {
            Invoice captured = null;

            invoiceRepositoryMock
                .Setup(r => r.AddAsync(It.IsAny<Invoice>()))
                .Callback<Invoice>(i => captured = i);

            var model = new CreateInvoiceViewModel
            {
                AppointmentId = 2,
                Amount = 200
            };

            await invoiceService.CreateAsync(model);

            Assert.IsNotNull(captured);
            Assert.AreEqual(2, captured.AppointmentId);
            Assert.AreEqual(200, captured.Amount);
            Assert.IsFalse(captured.IsPaid); 
        }

        
        [Test]
        public async Task MarkAsPaidAsync_InvoiceExists_ShouldUpdate()
        {
            var invoice = new Invoice
            {
                Id = 1,
                IsPaid = false
            };

            invoiceRepositoryMock
                .Setup(r => r.GetByIdAsync(1))
                .ReturnsAsync(invoice);

            await invoiceService.MarkAsPaidAsync(1);

            Assert.IsTrue(invoice.IsPaid);
            invoiceRepositoryMock.Verify(r => r.Update(invoice), Times.Once);
            invoiceRepositoryMock.Verify(r => r.SaveChangesAsync(), Times.Once);
        }

        
        [Test]
        public async Task MarkAsPaidAsync_NotFound_ShouldDoNothing()
        {
            invoiceRepositoryMock
                .Setup(r => r.GetByIdAsync(1))
                .ReturnsAsync((Invoice?)null);

            await invoiceService.MarkAsPaidAsync(1);

            invoiceRepositoryMock.Verify(r => r.Update(It.IsAny<Invoice>()), Times.Never);
            invoiceRepositoryMock.Verify(r => r.SaveChangesAsync(), Times.Never);
        }
    }
}