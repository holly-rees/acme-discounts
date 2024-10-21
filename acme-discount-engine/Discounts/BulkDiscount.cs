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

        public void ApplyDiscount(List<Item> items)
        {
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
                        for (int j = 0; j < 10; j++)
                        {
                            // go backwards and apply discount to each of the 10 items
                            Item bulkItem = items[i - j];
                            Money money = new Money(bulkItem.Price);
                            money.ApplyDiscountByPercent(2);
                            bulkItem.Price = money.getAmountAsDouble();
                        }
                        itemCount = 0;
                    }
                }
            }
        }


        public bool isEligibleForBulkDiscount(Item item, int itemCount)
        {
            return itemCount == 10 && !TwoForOneList.Contains(item.Name) && item.Price >= 5.00;
        }
    }


}