using AcmeSharedModels;
using acme_discount_engine.Money;

namespace acme_discount_engine.Discounts
{
    public class DiscountEngine
    {
        public bool LoyaltyCard { get; set; }
        public DateTime Time { get; set; } = DateTime.Now;

        private List<string> TwoForOneList = new List<string> { "Freddo" };
        private List<string> NoDiscount = new List<string> { "T-Shirt", "Keyboard", "Drill", "Chair" };

        public double ApplyDiscounts(List<Item> items)
        {
            SortAlphabetically(items);
            string currentItem = string.Empty;
            int itemCount = 0;

            // handle 2 for 1 discounts
            for (int i = 0; i < items.Count; i++)
            {
                Item item = items[i];
                if (item.Name != currentItem)
                {
                    currentItem = item.Name;
                    itemCount = 1;
                }
                else
                {
                    itemCount++;
                    if (itemCount == 3 && TwoForOneList.Contains(item.Name))
                    {
                        item.Price = 0.00;
                        itemCount = 0;
                    }
                }
            }
            //handle best before date discounts
            double itemTotal = 0.00;
            foreach (var item in items)
            {
                itemTotal += item.Price;

                int daysUntilDate = (item.Date - DateTime.Today).Days;
                if (DateTime.Today > item.Date) { daysUntilDate = -1; }
                if (!item.IsPerishable && isAllowedDiscount(item))
                {
                    ApplyNonPerishableItemDiscount(item, daysUntilDate);
                }
                else
                {
                    // perishable item discount
                    if (daysUntilDate == 0)
                    {
                        ApplyPerishableItemDiscount(item);
                    }
                }
            }

            currentItem = string.Empty;
            itemCount = 0;
            for (int i = 0; i < items.Count; i++)
            {
                Item item = items[i];
                if (item.Name != currentItem)
                {
                    currentItem = item.Name;
                    itemCount = 1;
                }
                else
                {
                    // handle bulk discount
                    itemCount++;
                    if (itemCount == 10 && !TwoForOneList.Contains(item.Name) && item.Price >= 5.00)
                    {
                        for (int j = 0; j < 10; j++)
                        {
                            items[i - j].Price -= items[i - j].Price * 0.02;
                        }
                        itemCount = 0;
                    }
                }
            }

            double finalTotal = items.Sum(item => item.Price);
            // loyal card discount
            if (LoyaltyCard && itemTotal >= 50.00)
            {
                finalTotal -= finalTotal * 0.02;
            }

            return Math.Round(finalTotal, 2);
        }

        public void SortAlphabetically(List<Item> items)
        {
            items.Sort((x, y) => x.Name.CompareTo(y.Name));
        }

        public bool isAllowedDiscount(Item item)
        {
            return !NoDiscount.Contains(item.Name);
        }

        public void ApplyPerishableItemDiscount(Item item)
        {
            if (Time.Hour >= 0 && Time.Hour < 12)
            {
                item.Price -= item.Price * 0.05;
            }
            else if (Time.Hour >= 12 && Time.Hour < 16)
            {
                item.Price -= item.Price * 0.10;
            }
            else if (Time.Hour >= 16 && Time.Hour < 18)
            {
                item.Price -= item.Price * 0.15;
            }
            else if (Time.Hour >= 18)
            {
                item.Price -= item.Price * (!item.Name.Contains("(Meat)") ? 0.25 : 0.15);
            }
        }

        public void ApplyNonPerishableItemDiscount(Item item, int daysUntilDate)
        {
            Money money = new Money(item.Price);


            if (daysUntilDate >= 6 && daysUntilDate <= 10)
            {
                item.Price -= item.Price * 0.05;
            }
            else if (daysUntilDate >= 0 && daysUntilDate <= 5)
            {
                item.Price -= item.Price * 0.10;
            }
            else if (daysUntilDate < 0)
            {
                item.Price -= item.Price * 0.20;
            }
        }
    }
}


