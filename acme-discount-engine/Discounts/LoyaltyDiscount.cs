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
        private bool LoyaltyCard;
        public LoyaltyDiscount(bool LoyaltyCard)
        {
            this.LoyaltyCard = LoyaltyCard;
        }
        public void ApplyTo(Basket basket)
        {
            Money finalTotal = new(basket.SumItems());

            Money totalAfter2for1 = basket.GetTotalAfter2for1();

            bool isOverLoyaltyThreshold = totalAfter2for1.getAmountAsDouble() >= 50.00;

            if (LoyaltyCard && isOverLoyaltyThreshold)
            {
                finalTotal.ApplyDiscountByPercent(2);
            }

            basket.UpdateRunningTotal(finalTotal.getAmount());
        }
    }
}