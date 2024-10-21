using AcmeSharedModels;
using acme_discount_engine.Models;

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
            List<IDiscountStrategy> discountStrategies = new List<IDiscountStrategy>
            {
                new TwoForOneDiscount(TwoForOneList),
                new UseByDateDiscount(NoDiscount, Time),
                new BulkDiscount(TwoForOneList)
            };

            DiscountManager discountManager = new DiscountManager(items, discountStrategies, LoyaltyCard);

            double finalAmount = discountManager.ApplyDiscounts();

            return finalAmount;

        }

    }

}


