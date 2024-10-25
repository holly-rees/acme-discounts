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
            string currentItem = string.Empty;
            int itemCount = 0;
            for (int i = 0; i < items.Count; i++)
            {
                Item item = items[i];
                if (isFirstOfNewItem(item, currentItem))
                {
                    InitialiseNewItemCounter(out currentItem, out itemCount, item);
                }
                else
                {
                    itemCount++;
                    if (isEligibleForBulkDiscount(item, itemCount))
                    {
                        ReducePriceForItemSet(items, i);
                        ResetCounter(itemCount);
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

        private bool isFirstOfNewItem(Item item, string currentItem)
        {
            return item.Name != currentItem;
        }

        private void InitialiseNewItemCounter(out string currentItem, out int itemCount, Item item)
        {
            currentItem = item.Name;
            itemCount = 1;
        }
        private void ResetCounter(int itemCount)
        {
            itemCount = 0;
        }
    }


}