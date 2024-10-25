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
            ItemCounter counter = new();

            foreach (Item item in items)
            {
                if (counter.isFirstOfNewItem(item))
                {
                    counter.initialiseNewItemCount(item);
                }
                else
                {
                    counter.increaseCount();
                    if (isEligibleFor2for1Discount(item, counter.getCount()))
                    {
                        DiscountPriceOf(item);
                        counter.ResetCounter();
                    }
                }
            }
        }

        private void DiscountPriceOf(Item item)
        {
            item.Price = 0.00;
        }

        private bool isEligibleFor2for1Discount(Item item, int itemCount)
        {
            return itemCount == 3 && TwoForOneList.Contains(item.Name);
        }
    }
}