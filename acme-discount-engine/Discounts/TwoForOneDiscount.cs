using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using AcmeSharedModels;
using acme_discount_engine.Models;

namespace acme_discount_engine.Discounts
{
    public class TwoForOneDiscount : IDiscountStrategy
    {
        private List<string> TwoForOneList;

        public TwoForOneDiscount(List<string> twoForOneList)
        {
            this.TwoForOneList = twoForOneList;
        }

        public void ApplyTo(List<Item> items, Money totalAfter2for1, Money runningTotal)
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
                    if (itemCount == 3 && TwoForOneList.Contains(item.Name))
                    {
                        item.Price = 0.00;
                        itemCount = 0;
                    }
                }
            }
            totalAfter2for1.AddMoney((decimal)items.Sum(item => item.Price));
            runningTotal.Reset();
            runningTotal.AddMoney((decimal)items.Sum(item => item.Price));
        }
    }
}