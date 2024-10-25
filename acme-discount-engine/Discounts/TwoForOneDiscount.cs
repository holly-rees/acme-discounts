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

        public void ApplyTo(Basket basket)
        {
            ApplyAny2for1Discounts(basket.GetItems());

            decimal totalAfterDiscount = basket.SumItems();

            basket.UpdateTotalAfter2for1(totalAfterDiscount);
            basket.UpdateRunningTotal(totalAfterDiscount);
        }

        private void ApplyAny2for1Discounts(List<Item> items)
        {
            string currentItem = "";
            int itemCount = 0;
            foreach (Item item in items)
            {
                if (isFirstOfNewItem(item, currentItem))
                {
                    InitialiseNewItemCounter(out currentItem, out itemCount, item);
                }
                else
                {
                    itemCount++;
                    if (isEligibleFor2for1Discount(itemCount, item))
                    {
                        DiscountPriceOf(item);
                        ResetCounter(itemCount);
                    }
                }
            }
        }

        private void ResetCounter(int itemCount)
        {
            itemCount = 0;
        }

        private void DiscountPriceOf(Item item)
        {
            item.Price = 0.00;
        }

        private bool isEligibleFor2for1Discount(int itemCount, Item item)
        {
            return itemCount == 3 && TwoForOneList.Contains(item.Name);
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
    }
}