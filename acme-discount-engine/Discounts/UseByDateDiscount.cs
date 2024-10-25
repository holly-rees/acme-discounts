using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using AcmeSharedModels;
using acme_discount_engine.Models;

namespace acme_discount_engine.Discounts
{
    public class UseByDateDiscount : IDiscountStrategy
    {
        private List<string> NoDiscount;

        public DateTime Time;

        public UseByDateDiscount(List<string> noDiscount, DateTime time)
        {
            NoDiscount = noDiscount;
            Time = time;
        }
        public void ApplyTo(Basket basket)
        {
            ApplyAnyUseByDateDiscounts(basket.GetItems());

            decimal totalAfterDiscount = basket.SumItems();
            basket.UpdateRunningTotal(totalAfterDiscount);
        }

        private void ApplyAnyUseByDateDiscounts(List<Item> items)
        {
            foreach (var item in items)
            {
                int daysUntilDate = CalculateDaysUntilUseByDate(item);
                int discountPercent = 0;
                Money price = new Money(item.Price);

                if (isAllowedNonPerishableDiscount(item))
                {
                    discountPercent = CalculateNonPerishableDiscountPercentage(item, daysUntilDate);
                }
                if (isAllowedPerishableDiscount(item, daysUntilDate))
                {
                    discountPercent = CalculatePerishableDiscountPercentage(item);
                }
                price.ApplyDiscountByPercent(discountPercent);
                item.Price = price.getAmountAsDouble();
            }
        }

        private int CalculateDaysUntilUseByDate(Item item)
        {
            int daysUntilDate = (item.Date - DateTime.Today).Days;

            if (isOutOfDate(item))
            {
                daysUntilDate = -1;
            }

            return daysUntilDate;
        }

        private bool isOutOfDate(Item item)
        {
            return DateTime.Today > item.Date;
        }

        private int CalculatePerishableDiscountPercentage(Item item)
        {
            if (Time.Hour >= 0 && Time.Hour < 12)
            {
                return 5;
            }
            if (Time.Hour >= 12 && Time.Hour < 16)
            {
                return 10;
            }
            if (Time.Hour >= 16 && Time.Hour < 18)
            {
                return 15;
            }
            if (Time.Hour >= 18)
            {
                if (!item.Name.Contains("(Meat)"))
                {
                    return 25;
                }
                return 15;
            }
            return 0;
        }

        private int CalculateNonPerishableDiscountPercentage(Item item, int daysUntilDate)
        {
            if (daysUntilDate >= 6 && daysUntilDate <= 10)
            {
                return 5;
            }
            else if (daysUntilDate >= 0 && daysUntilDate <= 5)
            {
                return 10;
            }
            else if (daysUntilDate < 0)
            {
                return 20;
            }
            return 0;
        }

        private bool isAllowedNonPerishableDiscount(Item item)
        {
            return !item.IsPerishable && !NoDiscount.Contains(item.Name);
        }

        private bool isAllowedPerishableDiscount(Item item, int daysUntilDate)
        {
            return item.IsPerishable && daysUntilDate == 0;
        }
    }
}