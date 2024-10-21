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

        private Money? totalAfter2for1Discount;

        private Money? finalTotal;

        private bool LoyaltyCard;

        public DiscountManager(List<Item> items, List<IDiscountStrategy> discountStrategies, bool LoyaltyCard)
        {
            this.items = items;
            this.discountStrategies = discountStrategies;
            this.LoyaltyCard = LoyaltyCard;
        }


        public double ApplyDiscounts()
        {
            SortAlphabetically();

            foreach (var strategy in discountStrategies)
            {
                strategy.ApplyDiscount(items);
                if (strategy is TwoForOneDiscount)
                {
                    totalAfter2for1Discount = new Money(items.Sum(item => item.Price));
                }
            }
            ApplyAnyLoyaltyDiscount();

            return finalTotal.getRoundedAmount(2);
        }

        public void SortAlphabetically()
        {
            items.Sort((x, y) => x.Name.CompareTo(y.Name));
        }
        public void ApplyAnyLoyaltyDiscount()
        {
            finalTotal = new Money(items.Sum(item => item.Price));

            bool isOverLoyaltyThreshold = totalAfter2for1Discount.getAmountAsDouble() >= 50.00;
            if (LoyaltyCard && isOverLoyaltyThreshold)
            {
                finalTotal.ApplyDiscountByPercent(2);
            }
        }
    }
}