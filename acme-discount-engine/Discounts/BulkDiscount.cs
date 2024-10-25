using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using AcmeSharedModels;
using acme_discount_engine.Models;

namespace acme_discount_engine.Discounts
{
    public class BulkDiscount : IDiscountStrategy
    {
        private List<string> TwoForOneList;

        public BulkDiscount(List<string> twoForOneList)
        {
            TwoForOneList = twoForOneList;
        }

        public void ApplyTo(Basket basket)
        {
            ApplyAnyBulkDiscounts(basket.GetItems());
            decimal totalAfterDiscount = basket.SumItems();
            basket.UpdateRunningTotal(totalAfterDiscount);
        }

        private void ApplyAnyBulkDiscounts(List<Item> items)
        {
            ItemCounter counter = new();
            for (int i = 0; i < items.Count; i++)
            {
                Item item = items[i];
                if (counter.isFirstOfNewItem(item))
                {
                    counter.initialiseNewItemCount(item);
                }
                else
                {
                    counter.increaseCount();
                    if (isEligibleForBulkDiscount(item, counter.getCount()))
                    {
                        ReducePriceForItemSet(items, i);
                        counter.ResetCounter();
                    }
                }
            }
        }

        private void ReducePriceForItemSet(List<Item> items, int currentIndex)
        {
            for (int j = 0; j < 10; j++)
            {
                Item bulkItem = items[currentIndex - j];
                Money money = new Money(bulkItem.Price);
                money.ApplyDiscountByPercent(2);
                bulkItem.Price = money.getAmountAsDouble();
            }
        }

        public bool isEligibleForBulkDiscount(Item item, int itemCount)
        {
            return itemCount == 10 && !TwoForOneList.Contains(item.Name) && item.Price >= 5.00;
        }
    }


}