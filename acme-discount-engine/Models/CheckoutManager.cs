using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using AcmeSharedModels;
using acme_discount_engine.Discounts;

namespace acme_discount_engine.Models
{
    public class CheckoutManager
    {
        private Basket basket;

        private List<IDiscountStrategy> discounts;

        public CheckoutManager(Basket basket, List<IDiscountStrategy> discountStrategies)
        {
            this.basket = basket;
            this.discounts = discountStrategies;
        }


        public void ApplyDiscounts()
        {
            foreach (var discount in discounts)
            {
                discount.ApplyTo(basket);
            }
        }
        public double GetRoundedTotal()
        {
            return basket.GetRunningTotal().getRoundedAmount(2);
        }

    }
}