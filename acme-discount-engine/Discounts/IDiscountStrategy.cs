using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using acme_discount_engine.Models;
using AcmeSharedModels;

namespace acme_discount_engine.Discounts
{
    public interface IDiscountStrategy
    {
        void ApplyTo(Basket basket);
    }
}