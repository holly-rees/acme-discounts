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
        public void ApplyDiscount(List<Item> items)
        {
            foreach (var item in items)
            {
                int daysUntilDate = (item.Date - DateTime.Today).Days;
                if (DateTime.Today > item.Date) { daysUntilDate = -1; }
                if (!item.IsPerishable && isAllowedDiscount(item))
                {
                    ApplyNonPerishableItemDiscount(item, daysUntilDate);
                }
                else
                {
                    if (daysUntilDate == 0)
                    {
                        ApplyPerishableItemDiscount(item);
                    }
                }
            }
        }

        public void ApplyPerishableItemDiscount(Item item)
        {
            Money money = new Money((decimal)item.Price);

            if (Time.Hour >= 0 && Time.Hour < 12)
            {
                money.ApplyDiscountByPercent(5);
            }
            else if (Time.Hour >= 12 && Time.Hour < 16)
            {
                money.ApplyDiscountByPercent(10);
            }
            else if (Time.Hour >= 16 && Time.Hour < 18)
            {
                money.ApplyDiscountByPercent(15);
            }
            else if (Time.Hour >= 18)
            {
                if (!item.Name.Contains("(Meat)"))
                {
                    money.ApplyDiscountByPercent(25);
                }
                else
                {
                    money.ApplyDiscountByPercent(15);
                }

            }
            item.Price = money.getAmountAsDouble();
        }

        public void ApplyNonPerishableItemDiscount(Item item, int daysUntilDate)
        {
            Money money = new Money(item.Price);


            if (daysUntilDate >= 6 && daysUntilDate <= 10)
            {
                money.ApplyDiscountByPercent(5);
            }
            else if (daysUntilDate >= 0 && daysUntilDate <= 5)
            {
                money.ApplyDiscountByPercent(10);
            }
            else if (daysUntilDate < 0)
            {
                money.ApplyDiscountByPercent(20);
            }
            item.Price = money.getAmountAsDouble();
        }

        public bool isAllowedDiscount(Item item)
        {
            return !NoDiscount.Contains(item.Name);
        }
    }
}