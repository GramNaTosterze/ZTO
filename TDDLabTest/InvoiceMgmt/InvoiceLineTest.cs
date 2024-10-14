using JetBrains.Annotations;
using TDDLab.Core.InvoiceMgmt;

namespace TDDLabTest.InvoiceMgmt;

[TestSubject(typeof(InvoiceLine))]
public class InvoiceLineTest
{
    private const string ValidProductName = "TestProductName";
    public static readonly InvoiceLine ValidLine = new InvoiceLine(ValidProductName, MoneyTest.ValidMoney);

    [Fact]
    public void ValidProductName_DoesNotContainValidationRule()
    {
        var line = ValidLine;
        Assert.DoesNotContain(InvoiceLine.ValidationRules.ProductName, line.Validate());
    }
    
    [Fact]
    public void EmptyProductName_DoesContainValidationRule()
    {
        var line = new InvoiceLine("", MoneyTest.ValidMoney);
        Assert.Contains(InvoiceLine.ValidationRules.ProductName, line.Validate());
    }

    [Fact]
    public void NullProductName_DoesNotContainValidationRule()
    {
        var line = new InvoiceLine(null, MoneyTest.ValidMoney);
        Assert.Contains(InvoiceLine.ValidationRules.ProductName, line.Validate());
    }
    
    [Fact]
    public void ValidMoney_DoesNotContainValidationRule()
    {
        var line = ValidLine;
        Assert.DoesNotContain(InvoiceLine.ValidationRules.Money, line.Validate());
    }
    
    [Fact]
    public void InvalidMoney_DoesContainValidationRule()
    {
        var line = new InvoiceLine(ValidProductName, new Money(100, ""));
        Assert.Contains(InvoiceLine.ValidationRules.Money, line.Validate());
    }

    [Fact]
    public void NullMoney_DoesNotContainValidationRule()
    {
        var line = new InvoiceLine(ValidProductName, null);
        Assert.Contains(InvoiceLine.ValidationRules.Money, line.Validate());
    }
}