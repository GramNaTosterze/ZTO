using JetBrains.Annotations;
using TDDLab.Core.InvoiceMgmt;

namespace TDDLabTest.InvoiceMgmt;

[TestSubject(typeof(Money))]
public class MoneyTest
{
    private const ulong ValidAmount = 10;
    private const string ValidCurrency = "zł";
    public static readonly Money ValidMoney = new Money(ValidAmount, ValidCurrency);

    [Fact]
    public void ValidCurrency_DoesNotContain()
    {
        var money = ValidMoney;
        Assert.DoesNotContain(Money.ValidationRules.Currency, money.Validate());
    }
    
    [Fact]
    public void EmptyCurrency_DoesContain()
    {
        var money = new Money(ValidAmount, "");
        Assert.Contains(Money.ValidationRules.Currency, money.Validate());
    }

    [Fact]
    public void NullCurrency_DoesContain()
    {
        var money = new Money(ValidAmount, null);
        Assert.Contains(Money.ValidationRules.Currency, money.Validate());
    }
    
    // operators
    [Fact]
    public void SubstractSmaller_EqualsValid()
    {
        var money = ValidMoney;
        Assert.Equal(new Money(7, ValidCurrency), ValidMoney - new Money(3));
    }
    
    [Fact]
    public void SubstractBigger_EqualsZero()
    {
        var money = ValidMoney;
        Assert.Equal(new Money(0, ValidCurrency), money - new Money(100, ValidCurrency));
    }

    [Fact]
    public void AddOverflow_Overflows()
    {
        var money = ValidMoney;
        Assert.Equal(new Money(9), new Money(ulong.MaxValue) + money);
    }
}