using acme_discount_engine.Models;
using AcmeSharedModels;

namespace itemCounter_tests;

public class Tests
{
    private ItemCounter itemCounter;
    private Item item1;
    private Item item2;




    [SetUp]
    public void Setup()
    {
        itemCounter = new ItemCounter();
        item1 = new Item("Thing", 20.00, false, new DateTime(2200, 5, 1));

        item2 = new Item("Other Thing", 5.00, false, new DateTime(2200, 5, 1));
    }

    [Test]
    public void TestItemCounterInitialisesWithZero()
    {
        Assert.That(itemCounter.getCount(), Is.EqualTo(0));
    }

    [Test]
    public void TestIncreaseCount()
    {
        itemCounter.increaseCount();
        Assert.That(itemCounter.getCount(), Is.EqualTo(1));
    }

    [Test]
    public void TestResetCounter()
    {
        itemCounter.increaseCount();
        itemCounter.increaseCount();
        itemCounter.ResetCounter();
        Assert.That(itemCounter.getCount(), Is.EqualTo(0));
    }

    [Test]
    public void TestInitialiseNewItemCount()
    {
        itemCounter.initialiseNewItemCount(item1);
        Assert.That(itemCounter.getCount(), Is.EqualTo(1));
        Assert.IsFalse(itemCounter.isFirstOfNewItem(item1));
    }

    [Test]
    public void TestFirstOfNewItemReturnsTrueForNewItem()
    {
        itemCounter.initialiseNewItemCount(item1);
        itemCounter.increaseCount();
        Assert.IsTrue(itemCounter.isFirstOfNewItem(item2));
    }

    [Test]
    public void TestFirstOfNewItemReturnsFalseForNotNewItem()
    {
        itemCounter.initialiseNewItemCount(item1);
        Assert.IsFalse(itemCounter.isFirstOfNewItem(item1));
    }
}