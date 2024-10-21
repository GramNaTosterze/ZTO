using JetBrains.Annotations;
using TDDLab.Core.InvoiceMgmt;

namespace TDDLabTest.InvoiceMgmt;

[TestSubject(typeof(Recipient))]
public class RecipientTest
{
    private const string ValidRecipientName = "TestRecipient";
    public static readonly Recipient ValidRecipient = new Recipient(ValidRecipientName, AddressTest.ValidAddress);
    
    [Fact]
    public void ValidName_DoesNotContain()
    {
        var recipient = ValidRecipient;
        Assert.DoesNotContain(Recipient.ValidationRules.Name, recipient.Validate());
    }
    
    [Fact]
    public void EmptyName_DoesContain()
    {
        var recipient = new Recipient("", AddressTest.ValidAddress);
        Assert.Contains(Recipient.ValidationRules.Name, recipient.Validate());
    }

    [Fact]
    public void NullName_DoesContain()
    {
        var recipient = new Recipient(null, AddressTest.ValidAddress);
        Assert.Contains(Recipient.ValidationRules.Name, recipient.Validate());
    }
    
    [Fact]
    public void ValidAddress_DoesNotContain()
    {
        var recipient = ValidRecipient;
        Assert.DoesNotContain(Recipient.ValidationRules.Address, recipient.Validate());
    }
    
    [Fact]
    public void InvalidAddress_DoesContain()
    {
        var recipient = new Recipient(ValidRecipientName, new Address("", "", "", ""));
        Assert.Contains(Recipient.ValidationRules.Address, recipient.Validate());
    }

    [Fact]
    public void NullAddress_ThrowsNullReferenceException()
    {
        var recipient = new Recipient(ValidRecipientName, null);
        Assert.Throws<NullReferenceException>(() => recipient.Validate());
    }
}