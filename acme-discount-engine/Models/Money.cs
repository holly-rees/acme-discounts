using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace acme_discount_engine.Models
{
    public class Money
    {
        private decimal _amount;

        public Money(decimal amount)
        {
            _amount = amount;
        }

        public Money(double amount)
        {
            _amount = (decimal)amount;
        }

        public decimal getAmount()
        {
            return _amount;
        }

        public double getAmountAsDouble()
        {
            return (double)_amount;
        }

        public double getRoundedAmount(int decimalPlaces)
        {
            return Math.Round((double)_amount, decimalPlaces);
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

        public void Reset()
        {
            _amount = 0;
        }
    }
}