using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using AcmeSharedModels;
using acme_discount_engine.Discounts;

namespace acme_discount_engine.Models
{
    public class DiscountManager
    {
        private List<Item> items;

        private List<IDiscountStrategy> discountStrategies;

        private Money totalAfter2for1Discount = new Money(0.0);

        private Money runningTotal = new Money(0.0);

        public DiscountManager(List<Item> items, List<IDiscountStrategy> discountStrategies)
        {
            this.items = items;
            this.discountStrategies = discountStrategies;
        }


        public double ApplyDiscounts()
        {
            SortAlphabetically();

            foreach (var discount in discountStrategies)
            {
                discount.ApplyTo(items, totalAfter2for1Discount, runningTotal);
            }
            return runningTotal.getRoundedAmount(2);
        }

        public void SortAlphabetically()
        {
            items.Sort((x, y) => x.Name.CompareTo(y.Name));
        }

    }
}