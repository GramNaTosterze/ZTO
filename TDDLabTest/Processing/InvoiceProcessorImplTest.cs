using JetBrains.Annotations;
using TDDLab.Core.InvoiceMgmt;
using TDDLabTest.InvoiceMgmt;

namespace TDDLabTest.Processing;

// simple mock for Invoice, made because neither Moq nor NSubstitute work :<  


[TestSubject(typeof(InvoiceProcessorImpl))]
public class InvoiceProcessorImplTest
{

    [Fact]
    public void Process_ShouldReturnFailed_WhenInvoiceIsInvalid()
    {
        // Arrange 
        var invoiceProcessor = new InvoiceProcessorImpl();
        var invoice = new Invoice();
        
        // Act
        var result = invoiceProcessor.Process(invoice);

        // Assert
        Assert.Equal(ProcessingResult.Failed(), result);
    }
    
    [Fact]
    public void Process_ShouldAddNewProduct_WhenProductDoesNotExist()
    {
        // Arrange
        var invoiceProcessor = new InvoiceProcessorImpl();

        var invoice = InvoiceTest.ValidInvoice;
        invoice.AttachInvoiceLine( new InvoiceLine("ProductA", new Money(100)));

        // Act
        invoiceProcessor.Process(invoice);

        // Assert
        Assert.True(invoiceProcessor.Products.ContainsKey("ProductA"));
        Assert.Equal(new Money(100), invoiceProcessor.Products["ProductA"]);
    }
    
    [Fact]
    public void Process_ShouldUpdateExistingProduct_WhenProductExists()
    {
        // Arrange
        var invoiceProcessor = new InvoiceProcessorImpl();

        var invoice = new Invoice("123", RecipientTest.ValidRecipient, AddressTest.ValidAddress, new List<InvoiceLine>(), new Money(10));
        invoice.AttachInvoiceLine( new InvoiceLine("ProductA", new Money(100)));
        
        invoiceProcessor.Process(invoice);

        var newInvoice = new Invoice("124", RecipientTest.ValidRecipient, AddressTest.ValidAddress, new List<InvoiceLine>(), new Money(5));
        newInvoice.AttachInvoiceLine( new InvoiceLine("ProductA", new Money(50)));

        // Act
        invoiceProcessor.Process(newInvoice);

        // Assert
        Assert.Equal(new Money(145), invoiceProcessor.Products["ProductA"]);
    }
}