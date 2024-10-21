using JetBrains.Annotations;
using TDDLab.Core.InvoiceMgmt;

namespace TDDLabTest.InvoiceMgmt;

[TestSubject(typeof(Invoice))]
public class InvoiceTest
{
    private const string ValidInvoiceNumber = "123";
    private static readonly IEnumerable<InvoiceLine> ValidLines = [InvoiceLineTest.ValidLine];
    public static readonly Invoice ValidInvoice = new Invoice(ValidInvoiceNumber, RecipientTest.ValidRecipient, AddressTest.ValidAddress, ValidLines);
    
    
    [Fact]
    public void ValidInvoiceNumber_DoesNotContainValidationRule()
    {
        var invoice = ValidInvoice;
        Assert.DoesNotContain(Invoice.ValidationRules.InvoiceNumber, invoice.Validate());
    }
    
    [Fact]
    public void EmptyInvoiceNumber_DoesContainValidationRule()
    {
        var invoice = new Invoice("", RecipientTest.ValidRecipient, AddressTest.ValidAddress, ValidLines, MoneyTest.ValidMoney);
        Assert.Contains(Invoice.ValidationRules.InvoiceNumber, invoice.Validate());
    }

    [Fact]
    public void NullInvoiceNumber_DoesContainValidationRule()
    {
        var invoice = new Invoice("", RecipientTest.ValidRecipient, AddressTest.ValidAddress, ValidLines, MoneyTest.ValidMoney);
        Assert.Contains(Invoice.ValidationRules.InvoiceNumber, invoice.Validate());
    }
    
    [Fact]
    public void ValidBillingAddress_DoesNotContainValidationRule()
    {
        var invoice = ValidInvoice;
        Assert.DoesNotContain(Invoice.ValidationRules.BillingAddress, invoice.Validate());
    }
    
    [Fact]
    public void InvalidBillingAddress_DoesContainValidationRule()
    {
        var invoice = new Invoice(ValidInvoiceNumber, RecipientTest.ValidRecipient, new Address("", "", "", ""), ValidLines, MoneyTest.ValidMoney);
        Assert.Contains(Invoice.ValidationRules.BillingAddress, invoice.Validate());
    }

    [Fact]
    public void NullBillingAddress_DoesContainValidationRule()
    {
        var invoice = new Invoice(ValidInvoiceNumber, RecipientTest.ValidRecipient, null, ValidLines, MoneyTest.ValidMoney);
        Assert.Contains(Invoice.ValidationRules.BillingAddress, invoice.Validate());
    }
    
    [Fact]
    public void ValidRecipient_DoesNotContainValidationRule()
    {
        var invoice = ValidInvoice;
        Assert.DoesNotContain(Invoice.ValidationRules.Recipient, invoice.Validate());
    }
    
    [Fact]
    public void InvalidRecipient_DoesContainValidationRule()
    {
        var invoice = new Invoice(ValidInvoiceNumber, new Recipient("", AddressTest.ValidAddress), AddressTest.ValidAddress, ValidLines, MoneyTest.ValidMoney);
        Assert.Contains(Invoice.ValidationRules.Recipient, invoice.Validate());
    }

    [Fact]
    public void NullRecipient_DoesContainValidationRule()
    {
        var invoice = new Invoice(ValidInvoiceNumber, null, AddressTest.ValidAddress, ValidLines, MoneyTest.ValidMoney);
        Assert.Contains(Invoice.ValidationRules.Recipient, invoice.Validate());
    }
    
    [Fact]
    public void ValidDiscount_DoesNotContainValidationRule()
    {
        var invoice = ValidInvoice;
        Assert.DoesNotContain(Invoice.ValidationRules.Discount, invoice.Validate());
    }
    
    [Fact]
    public void InvalidDiscount_DoesContainValidationRule()
    {
        var invoice = new Invoice(ValidInvoiceNumber, RecipientTest.ValidRecipient, AddressTest.ValidAddress, ValidLines, new Money(0, null));
        Assert.Contains(Invoice.ValidationRules.Discount, invoice.Validate());
    }

    [Fact]
    public void NullDiscount_DoesNotContainValidationRule()
    {
        var invoice = new Invoice(ValidInvoiceNumber, RecipientTest.ValidRecipient, AddressTest.ValidAddress, ValidLines, null);
        Assert.DoesNotContain(Invoice.ValidationRules.Discount, invoice.Validate());
    }
    
    [Fact]
    public void ValidLines_DoesNotContainValidationRule()
    {
        var invoice = ValidInvoice;
        Assert.DoesNotContain(Invoice.ValidationRules.Lines, invoice.Validate());
    }
    
    [Fact]
    public void InvalidLines_DoesContainValidationRule()
    {
        var invoice = new Invoice(ValidInvoiceNumber, RecipientTest.ValidRecipient, AddressTest.ValidAddress, [new InvoiceLine("", MoneyTest.ValidMoney)], MoneyTest.ValidMoney);
        Assert.Contains(Invoice.ValidationRules.Lines, invoice.Validate());
    }

    [Fact]
    public void EmptyLines_DoesNotContainValidationRule()
    {
        var invoice = new Invoice(ValidInvoiceNumber, RecipientTest.ValidRecipient, AddressTest.ValidAddress, [], MoneyTest.ValidMoney);
        Assert.Contains(Invoice.ValidationRules.Lines, invoice.Validate());
    }
    
    [Fact]
    public void NullLines_DoesNotContainValidationRule()
    {
        var invoice = new Invoice(ValidInvoiceNumber, RecipientTest.ValidRecipient, AddressTest.ValidAddress, null, MoneyTest.ValidMoney);
        Assert.Contains(Invoice.ValidationRules.Lines, invoice.Validate());
    }
}