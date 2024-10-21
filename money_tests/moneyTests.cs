using acme_discount_engine.Money;
namespace money_tests;

public class Tests
{
    private Money money;
    [SetUp]
    public void Setup()
    {
        money = new Money(0);
    }

    [Test]
    public void TestAddMoney()
    {
        money.AddMoney(12);
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
}