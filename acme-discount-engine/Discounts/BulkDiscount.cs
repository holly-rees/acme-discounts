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
            List<Item> items = basket.GetItems();
            string currentItem = string.Empty;
            int itemCount = 0;
            for (int i = 0; i < items.Count; i++)
            {
                Item item = items[i];
                if (item.Name != currentItem)
                {
                    currentItem = item.Name;
                    itemCount = 1;
                }
                else
                {
                    itemCount++;
                    if (isEligibleForBulkDiscount(item, itemCount))
                    {
                        ApplyBulkDiscount(items, i);
                        itemCount = 0;
                    }
                }
            }
            decimal totalAfterDiscount = basket.SumItems();
            basket.UpdateRunningTotal(totalAfterDiscount);
        }

        private void ApplyBulkDiscount(List<Item> items, int currentIndex)
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