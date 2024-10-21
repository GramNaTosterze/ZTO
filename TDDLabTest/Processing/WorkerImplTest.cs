using JetBrains.Annotations;
using Moq;
using TDDLab.Core.Infrastructure;
using TDDLab.Core.InvoiceMgmt;

namespace TDDLabTest.Processing;

[TestSubject(typeof(WorkerImpl))]
public class WorkerImplTest
{

    [Fact]
    public void Start_ShouldInitializeInputAndOutputChannels()
    {
        // Arrange
        var configurationSettingsMock = new Mock<IConfigurationSettings>();
        var messagingFacilityMock = new Mock<IMessagingFacility<Invoice, ProcessingResult>>();
        var exceptionHandlerMock = new Mock<IExceptionHandler>();
        var invoiceProcessorMock = new Mock<IInvoiceProcessor>();

        configurationSettingsMock.Setup(cs => cs.GetSettingsByKey("inputQueue")).Returns("inputQueueValue");
        configurationSettingsMock.Setup(cs => cs.GetSettingsByKey("outputQueue")).Returns("outputQueueValue");

        var worker = new WorkerImpl(
            configurationSettingsMock.Object,
            messagingFacilityMock.Object,
            exceptionHandlerMock.Object,
            invoiceProcessorMock.Object);

        // Act
        worker.Start();

        // Assert
        messagingFacilityMock.Verify(mf => mf.InitializeInputChannel("inputQueueValue"), Times.Once);
        messagingFacilityMock.Verify(mf => mf.InitializeOutputChannel("outputQueueValue"), Times.Once);
    }

    [Fact]
    public void Stop_ShouldDisposeMessagingFacility()
    {
        // Arrange
        var configurationSettingsMock = new Mock<IConfigurationSettings>();
        var messagingFacilityMock = new Mock<IMessagingFacility<Invoice, ProcessingResult>>();
        var exceptionHandlerMock = new Mock<IExceptionHandler>();
        var invoiceProcessorMock = new Mock<IInvoiceProcessor>();

        var worker = new WorkerImpl(
            configurationSettingsMock.Object,
            messagingFacilityMock.Object,
            exceptionHandlerMock.Object,
            invoiceProcessorMock.Object);

        // Act
        worker.Stop();

        // Assert
        messagingFacilityMock.Verify(mf => mf.Dispose(), Times.Once);
    }

    [Fact]
    public void DoJob_ShouldProcessInvoiceAndWriteMessage()
    {
        // Arrange
        var configurationSettingsMock = new Mock<IConfigurationSettings>();
        var messagingFacilityMock = new Mock<IMessagingFacility<Invoice, ProcessingResult>>();
        var exceptionHandlerMock = new Mock<IExceptionHandler>();
        var invoiceProcessorMock = new Mock<IInvoiceProcessor>();

        var invoice = new Invoice();
        var processingResult = ProcessingResult.Succeeded();
        var message = new Message<Invoice> { Data = invoice, Metadata = new Metadata() };

        messagingFacilityMock.Setup(mf => mf.ReadMessage()).Returns(message);
        invoiceProcessorMock.Setup(ip => ip.Process(invoice)).Returns(processingResult);

        var worker = new WorkerImpl(
            configurationSettingsMock.Object,
            messagingFacilityMock.Object,
            exceptionHandlerMock.Object,
            invoiceProcessorMock.Object);

        // Act
        worker.DoJob();

        // Assert
        messagingFacilityMock.Verify(mf => mf.ReadMessage(), Times.Once);
        invoiceProcessorMock.Verify(ip => ip.Process(invoice), Times.Once);
        messagingFacilityMock.Verify(mf => mf.WriteMessage(It.Is<Message<ProcessingResult>>(m => m.Data == processingResult && m.Metadata == message.Metadata)), Times.Once);
    }

    [Fact]
    public void DoJob_ShouldHandleException()
    {
        // Arrange
        var configurationSettingsMock = new Mock<IConfigurationSettings>();
        var messagingFacilityMock = new Mock<IMessagingFacility<Invoice, ProcessingResult>>();
        var exceptionHandlerMock = new Mock<IExceptionHandler>();
        var invoiceProcessorMock = new Mock<IInvoiceProcessor>();

        var exception = new Exception("Test exception");

        messagingFacilityMock.Setup(mf => mf.ReadMessage()).Throws(exception);

        var worker = new WorkerImpl(
            configurationSettingsMock.Object,
            messagingFacilityMock.Object,
            exceptionHandlerMock.Object,
            invoiceProcessorMock.Object);

        // Act
        worker.DoJob();

        // Assert
        exceptionHandlerMock.Verify(eh => eh.HandleException(exception), Times.Once);
    }
}