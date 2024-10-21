using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using AcmeSharedModels;
using acme_discount_engine.Models;

namespace acme_discount_engine.Discounts
{
    public class LoyaltyDiscount : IDiscountStrategy
    {
        public void ApplyDiscount(List<Item> items)
        {
            // bool isOverLoyaltyThreshold = (totalAfter2for1Discount.getAmountAsDouble() >= 50.00);
            // if (LoyaltyCard && isOverLoyaltyThreshold)
            // {
            //     finalTotal.ApplyDiscountByPercent(2);
            // }
        }
    }
}