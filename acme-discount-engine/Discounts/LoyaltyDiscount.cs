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
        private Money finalTotal = new Money(0.0);
        public LoyaltyDiscount(bool LoyaltyCard)
        {
            this.LoyaltyCard = LoyaltyCard;
        }
        public void ApplyTo(List<Item> items, Money totalAfter2for1, Money runningTotal)
        {
            finalTotal.AddMoney((decimal)items.Sum(item => item.Price));
            bool isOverLoyaltyThreshold = totalAfter2for1.getAmountAsDouble() >= 50.00;
            if (LoyaltyCard && isOverLoyaltyThreshold)
            {
                finalTotal.ApplyDiscountByPercent(2);
            }
            runningTotal.Reset();
            runningTotal.AddMoney(finalTotal.getAmount());
        }
    }
}