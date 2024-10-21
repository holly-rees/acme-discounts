using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using AcmeSharedModels;

namespace acme_discount_engine.Discounts
{
    public interface IDiscountStrategy
    {
        void ApplyDiscount(List<Item> items);
    }
}