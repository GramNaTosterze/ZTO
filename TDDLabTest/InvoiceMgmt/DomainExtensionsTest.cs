using JetBrains.Annotations;
using TDDLab.Core.InvoiceMgmt;

namespace TDDLabTest.InvoiceMgmt;

[TestSubject(typeof(DomainExtensions))]
public class DomainExtensionsTest
{
    
    [Fact]
    public void ValidMoney_ValidOutput()
    {
        var expected = new Money(10, "zł");
        var testData = new Money(10, "USD");
        var actual = DomainExtensions.ToCurrency(testData, "zł");
        Assert.Equal(actual, expected);
    }
    
    [Fact]
    public void NullMoney_ThrowsNullReferenceException()
    {
        Assert.Throws<NullReferenceException>(() => DomainExtensions.ToCurrency(null, "zł"));
    }
    
    [Fact]
    public void NullCurrency_ValidOutput()
    {
        var expected = new Money(10, null);
        var testData = new Money(10, "zł");
        Assert.Equal(expected, DomainExtensions.ToCurrency(testData, null));
    }
    
    [Fact]
    public void EmptyCurrency_ValidOutput()
    {
        var expected = new Money(10, "");
        var testData = new Money(10, "USD");
        Assert.Equal(expected, DomainExtensions.ToCurrency(testData, ""));
    }
}