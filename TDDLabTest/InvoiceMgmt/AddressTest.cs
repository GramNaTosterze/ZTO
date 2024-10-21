using JetBrains.Annotations;
using TDDLab.Core.InvoiceMgmt;

namespace TDDLabTest.InvoiceMgmt;

[TestSubject(typeof(Address))]
public class AddressTest
{
    private const string ValidAddresLine = "Address 02";
    private const string ValidCity = "City";
    private const string ValidState = "State";
    private const string ValidPostalCode = "PostalCode";
    public static readonly Address ValidAddress = new Address(ValidAddresLine, ValidCity, ValidState, ValidPostalCode);
    
    [Fact]
    public void ValidAddressLine_DoesNotContainValidationRule()
    {
        var address = ValidAddress;
        Assert.DoesNotContain(Address.ValidationRules.AddressLine1, address.Validate());
    }
    
    [Fact]
    public void EmptyAddressLine_DoesContainValidationRule()
    {
        var address = new Address("", ValidCity, ValidState, ValidPostalCode);
        Assert.Contains(Address.ValidationRules.AddressLine1, address.Validate());
    }

    [Fact]
    public void NullAddressLine_DoesContainValidationRule()
    {
        var address = new Address(null, ValidCity, ValidState, ValidPostalCode);
        Assert.Contains(Address.ValidationRules.AddressLine1, address.Validate());
    }
    
    
    [Fact]
    public void ValidCity_DoesNotContainValidationRule()
    {
        var address = ValidAddress;
        Assert.DoesNotContain(Address.ValidationRules.City, address.Validate());
    }
    
    [Fact]
    public void EmptyCity_DoesContainValidationRule()
    {
        var address = new Address(ValidAddresLine, "", ValidState, ValidPostalCode);
        Assert.Contains(Address.ValidationRules.City, address.Validate());
    }

    [Fact]
    public void NullCity_DoesContainValidationRule()
    {
        var address = new Address(ValidAddresLine, null, ValidState, ValidPostalCode);
        Assert.Contains(Address.ValidationRules.City, address.Validate());
    }
    
    
    [Fact]
    public void ValidState_DoesNotContainValidationRule()
    {
        var address = ValidAddress;
        Assert.DoesNotContain(Address.ValidationRules.State, address.Validate());
    }
    
    [Fact]
    public void EmptyState_DoesContainValidationRule()
    {
        var address = new Address("", ValidCity, "", ValidPostalCode);
        Assert.Contains(Address.ValidationRules.State, address.Validate());
    }

    [Fact]
    public void NullState_DoesContainValidationRule()
    {
        var address = new Address(ValidAddresLine, ValidCity, null, ValidPostalCode);
        Assert.Contains(Address.ValidationRules.State, address.Validate());
    }
    
    
    [Fact]
    public void ValidZip_DoesNotContainValidationRule()
    {
        var address = ValidAddress;
        Assert.DoesNotContain(Address.ValidationRules.Zip, address.Validate());
    }
    
    [Fact]
    public void EmptyZip_DoesContainValidationRule()
    {
        var address = new Address(ValidAddresLine, ValidCity, ValidState, "");
        Assert.Contains(Address.ValidationRules.Zip, address.Validate());
    }

    [Fact]
    public void NullZip_DoesContainValidationRule()
    {
        var address = new Address(ValidAddresLine, ValidCity, ValidState, null);
        Assert.Contains(Address.ValidationRules.Zip, address.Validate());
    }
}