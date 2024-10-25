using acme_discount_engine.Models;
namespace money_tests;

public class Tests
{
    private Money money;
    [SetUp]
    public void Setup()
    {
        money = new Money(0.00);
    }

    [Test]
    public void TestAddMoney()
    {
        money.AddMoney(12.00);
        decimal amount = money.getAmount();
        Assert.That(amount, Is.EqualTo(12.00));
    }

    [Test]
    public void TestApplyDiscountByPercent()
    {
        money.AddMoney(10);
        money.ApplyDiscountByPercent(5);
        decimal amount = money.getAmount();
        Assert.That(amount, Is.EqualTo(9.50));
    }

    [Test]
    public void TestComplexTransactionWithReset()
    {
        money.AddMoney(200);
        money.ApplyDiscountByPercent(10);
        money.AddMoney(50);
        money.ApplyDiscountByPercent(5);
        money.Reset();
        money.AddMoney(100);
        money.ApplyDiscountByPercent(15);
        Assert.That(money.getAmount(), Is.EqualTo(85));
    }
}