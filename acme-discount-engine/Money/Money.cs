using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace acme_discount_engine.Money
{
    public class Money
    {
        private decimal _amount;

        public Money(decimal amount)
        {
            _amount = amount;
        }

        public decimal getAmount()
        {
            return _amount;
        }

        public void AddMoney(decimal money)
        {
            _amount += money;
        }

        public void ApplyDiscountByPercent(decimal percentage)
        {
            decimal multiplier = percentage / 100;
            _amount -= multiplier * _amount;
        }
    }
}